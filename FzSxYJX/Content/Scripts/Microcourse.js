/**
 * Created by tyx on 2017-12-26.
 */

$(function () {  
    $(".left #second").html($(".check span").text());
    /*初始化滑块宽度*/
    //$('.minNav .navslip').width($('.minNav ul li.cur').width() + 10);
    /*二级菜单
    一级点击事件*/
    $('.sidebar>ul>li>span').bind('click', function () {
        if ($(this).parent().hasClass('cur')) {
            $(this).parent().removeClass('cur');          
            $(".left #three").hide().prev().hide();
            $(".left #second").html($(".check span").text());
            $(".searchMsg").val("搜你想要...");
            if ($(this).parent().find('li').hasClass('cur')) {
                $(this).parent().find('li').removeClass('cur');
                GetCataIds();
            }
            //$('.resList').find('ul').hide().end().find('div').show();
        } else {
            $(this).parent().find('li').removeClass('cur');
            $(this).parent().siblings().removeClass('cur').removeClass('check').end().addClass('cur').addClass('check');
            $(".left #three").hide().prev().hide();
            $(".left #second").html($(".check span").text());
            $(".searchMsg").val("搜你想要...");
            //$('.resList').find('ul').hide().end().find('div').show();
            GetCataIds();
        }           
    })
    /*二级点击事件*/
    $('.sidebar>ul>li>ul>li').bind('click', function () {
        $(this).siblings().removeClass('cur').end().addClass('cur');     
        $(".left #three").show().prev().show();
        $(".left #second").show().prev().show();
        $(".left #second").html($(".check span").text());
        $(".left #three").html($(".check ul li.cur").text());
        $(".searchMsg").val("搜你想要...");
        $('.resList').find('ul').hide().end().find('div').show();
        GetCataIds();
    })

    /*search事件*/
    $('.searchAct').bind('click', function () {
        if ($(this).next().val() === '') {
            // alert("请输入关键字!");
            $(".left #three").hide().prev().hide();
            $(".left #second").hide().prev().hide();
            GetMicroCourse();
        } else {
            if ($(this).next().val() === "搜你想要...") {
                alert("请输入关键字!");
                return;
            } else {
                $(".left #three").hide().prev().hide();
                $(".left #second").hide().prev().hide();
                GetMicroCourse();
            }
        }
    })
    /*输入关键字*/
    $('.searchMsg').bind('focus', function () {
       // $(this).parent().addClass('set');
        if ($(this).val() === "搜你想要...") {
            $(this).val('');
           // $(this).val('').addClass('reset');
        }
    })
    //    .bind('blur', function () {
    //    //$(this).parent().removeClass('set');
    //    if ($(this).val() === "") {
    //        $(this).val("搜你想要...");
    //        // $(this).val("搜你想要...").removeClass('reset');
    //        //$('.resList').find('ul').hide().end().find('div').show();
    //        // $('.resList').find('ul').hide().end().find('div').show();
    //        $(".left #three").hide().prev().hide();
    //        $(".left #second").hide().prev().hide();
    //        GetMicroCourse();

    //    } else {
    //        // $(this).parent().addClass('set');
    //        $(".left #three").hide().prev().hide();
    //        $(".left #second").hide().prev().hide();
    //        GetMicroCourse();
    //    }
    //})
    $(".searchMsg").keydown(function (event) {
        if (event.keyCode == 13) {
            $(".left #three").hide().prev().hide();
            $(".left #second").hide().prev().hide();
            GetMicroCourse();
        }
    });
    gotoTop();
    /*通过知识点ID获取微课资源*/
    GetCataIds();

})
//////////////获取点击目录id集//////////
function GetCataIds(key) {
    var obj;
    var parentid = $(".sidebar .check").find("span").attr("id");
    var cataid = $(".sidebar .check ul").find(".cur").attr("id");
    if (cataid) {
        obj = {
            id: cataid
        }
    } else {
        obj = {
            id: parentid
        }
    }
    $.post('/MicroCourse/GetKnowledgeIDs', obj, function (data) {
        GetWKResource(data);
    });
}
//////////////获取微课资源////////////
function GetWKResource(ids) {
    var resourcehtml = '';
    $.post('/MicroCourse/GetMicroCourseByKnowledgeID', { cids: ids}, function (data) {
        if (data.Success) {
            $(".minNav").html('<span>共<b>' + data.Data.resourceCount + '</b>个资源</span>');
            if (data.Data.microResourceList && data.Data.resourceCount > 0) {
                resourcehtml += '<ul>';
                $.each(data.Data.microResourceList, function (e, res) {
                    resourcehtml += '<li>';
                    resourcehtml += '<span id="' + res.FileID + '" url="' + data.Data.FileUrl + '">';
                    resourcehtml += ' <b><img src="' + data.Data.FileUrl + 'GetFileImg.ashx?fileID=' + res.FileID + '&view=true" alt=""></b>';
                    resourcehtml += '<label>' + res.Title + '</label>';
                    resourcehtml += '<p><input class="view" type="button" value="预览">';
                    resourcehtml += '<input class="download" type="button" value="下载"></p>';
                    resourcehtml += ' </span>';
                    resourcehtml += '</li>';
                });
                resourcehtml += '</ul>';
            } else {
                resourcehtml += '<div class="nothing"></div>';
            }

        } else {
            resourcehtml += '<div class="nothing"></div>';
        }
        $(".content .resList").html(resourcehtml);
        LoadViewandDownload();
    });
}

////////////搜索整个学段微课////////
function GetMicroCourse() {

    var key = $(".searchMsg").val();
    if (key === "搜你想要...") {
        key = "";
    }
    var resourcehtml = '';
    $.post('/MicroCourse/GetMicroCourseByKey', { key: key }, function (data) {
        if (data.Success) {
            $(".minNav").html('<ul><li>' + key + '</li></ul><span>共<b>' + data.Data.resourceCount + '</b>个资源</span>');
            if (data.Data.microResourceList && data.Data.resourceCount > 0) {
                resourcehtml += '<ul>';
                $.each(data.Data.microResourceList, function (e, res) {
                    resourcehtml += '<li>';
                    resourcehtml += '<span id="' + res.FileID + '" url="' + data.Data.FileUrl + '">';
                    resourcehtml += ' <b><img src="' + data.Data.FileUrl + 'GetFileImg.ashx?fileID=' + res.FileID + '&view=true" alt=""></b>';
                    resourcehtml += '<label>' + res.Title + '</label>';
                    resourcehtml += '<p><input class="view" type="button" value="预览">';
                    resourcehtml += '<input class="download" type="button" value="下载"></p>';
                    resourcehtml += ' </span>';
                    resourcehtml += '</li>';
                });
                resourcehtml += '</ul>';
            } else {
                resourcehtml += '<div class="nothing"></div>';
            }

        } else {
            resourcehtml += '<div class="nothing"></div>';
        }
        $(".content .resList").html(resourcehtml);
        LoadViewandDownload();
    });
}

//////////////调用弹出预览框///////////
function ViewDialog(url) {
    DialogC.init();
    DialogC.setSkin('skin01');
    DialogC.addCont(dialogFn(url));
}
///////////////加载预览和下载///////////
function LoadViewandDownload() {
    /*资源预览*/
    $("input.view").unbind('click');
    $("input.view").bind('click', function () {
        var fileurl = $(this).parent().parent().attr("url");
        var fileID = $(this).parent().parent().attr("id");
        laodResourcePreview(fileID, fileurl, function (response) {
            if (response.CanPreview) {
                ViewDialog(response.URL);
            }
        });
    })

    $("input.view").parents("span").find("b").unbind("click");
    $("input.view").parents("span").find("b").bind('click', function () {
        var fileurl = $(this).parent().attr("url");
        var fileID = $(this).parent().attr("id");
        laodResourcePreview(fileID, fileurl, function (response) {
            if (response.CanPreview) {
                ViewDialog(response.URL);
            }
        });
    })
    //////////////异步回调加载预览资源///////////
    function laodResourcePreview(fileID, fileurl, callback) {
        var sendValues = { FileID: fileID };
        var url = fileurl + "Preview.ashx";
        $.ajax({
            type: "POST",
            url: url,
            data: sendValues,
            dataType: "jsonp",
            success: function (response) {
                callback(response);
            },
            error: function (request, status, error) {
            }
        });
    }
    /*资源下载*/
    $("input.download").unbind('click');
    $("input.download").bind('click', function () {
        var fileID = $(this).parents("span").attr("id");
        var fileurl = $(this).parents("span").attr("url");
        var name = $(this).parents("span").find("label").html();
        var url = fileurl + "GetFiles.ashx?FileID=" + fileID + "&name=" + escape(name);
        window.open(url);
    })
}