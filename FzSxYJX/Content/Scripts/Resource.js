var Resource = function () {
    var Current = this;

    this.BookID = 0;
    this.CataID = "";
    this.FileUrl = "";
    this.ChildIDStr = "";
    this.ResourceList = [];

    this.Init = function () {
        Current.BookID = Current.GetUrlParam("BookID");
        Current.FileUrl = $(".resList").attr("name");
        Current.ShowCata();
        ///////////////绑定目录点击事件///////////////////
        Current.BindCataClick();
        ///////////////搜索点击事件///////////////////
        Current.BindSearchClick();
        gotoTop();
        /*返回上一页*/
        $('#return').bind('click', function () {
            window.history.go(-1);
        });
    };
    this.GetUrlParam=function(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
    this.ShowCata = function () {
        $(".sidebar ul li").each(function(l,li) {
            var cataname = $.trim($(li).find("span").text());
            cataname = cutStr(cataname, strNum(cataname,18),18);
            $(li).find("span").text(cataname);
            
            if (typeof ($(li).find("ul")) != "undefined" && $(li).find("ul").length!=0) {
                $(li).find("span").append('<em class="em"></em>');
                $($(li).find("ul li")).each(function (c,child) {
                    var cataname = $.trim($(child).text());
                    cataname = cutStr(cataname, strNum(cataname,22),22);
                    $(child).text(cataname);
                });
            }
        });
    };
    //////////////////////////////////////////////////
    ///////////////绑定目录点击事件///////////////////
    //////////////////////////////////////////////////
    this.BindCataClick = function () {
        var CataName = $($(".sidebar>ul>li")[0]).attr("name");
        $($(".searchM .left")[0]).append('<span><a href="#">学习资源</a></span><em></em><span title="' + CataName + '">' + CataName + '</span>');
        /*一级点击事件*/
        $('.sidebar>ul>li>span').unbind('click');
        $('.sidebar>ul>li>span').bind('click', function () {
            $(this).parent().find("ul>li").siblings().removeClass('cur');
            if ($(this).parent().hasClass('cur')) {
                $(this).parent().removeClass('cur');
            } else {
                $(this).parent().siblings().removeClass('cur').removeClass('check').end().addClass('cur').addClass('check');
            }
            Current.ChildIDStr = "";
            $(".searchM .left").empty();
            var CataName = $(this).parent().attr("name");
            var mode = this;

            $($(".searchM .left")[0]).append('<span><a href="#">学习资源</a></span><em></em><span title="' + CataName + '">' + CataName + '</span>');
            if ($(".searchAct").next().val() != "搜你想要...") {
                $(".searchMsg").val("搜你想要...")
                //$(".searchMsg").val("搜你想要...").removeClass('reset').parent().removeClass('set');
                $('.resList').find('ul').hide().end().find('div').show();
                ///////////////加载资源类型列表///////////////////
                Current.InitResourceTypeList("",function() {
                    Current.CataID = $(mode).parent().attr("id");
                    $($('.minNav ul li')[0]).click();
                });
            } else {
                ///////////////加载资源类型列表///////////////////
                Current.InitResourceTypeList("",function () {
                    Current.CataID = $(mode).parent().attr("id");
                    var ResourceType = $($('.minNav ul li.cur')[0]).attr("id");
                    var ResourceStyle = $($('.minNav ul li.cur')[0]).attr("parent");
                    Current.InitResourceList(ResourceType, ResourceStyle);
                });
            }
        });
        /*二级点击事件*/
        $('.sidebar>ul>li>ul>li').unbind('click');
        $('.sidebar>ul>li>ul>li').bind('click', function () {
            $(this).siblings().removeClass('cur').end().addClass('cur');
            var ParentName = $(this).parent().parent().attr("name");
            var CataName = $(this).attr("name");

            $($(".searchM .left").find("span")[1]).remove();
            if ($(".searchM .left>span").length < 2) {
                $($(".searchM .left")[0]).append('<span title="' + ParentName + '">' + ParentName + "</span>").append('<em></em><span title="' + CataName + '">' + cutStr(CataName, strNum(CataName,18),18) + "</span>");
            } else {
                $(".searchM .left").empty();
                $($(".searchM .left")[0]).append('<span><a href="#">学习资源</a></span><em></em><span title="' + ParentName + '">' + ParentName + "</span>").append('<em></em><span title="' + CataName + '">' + cutStr(CataName, strNum(CataName,22),22) + "</span>");
            }
            Current.CataID = $(this).attr("id");
            if ($(".searchAct").next().val() != "搜你想要...") {
                $(".searchMsg").val("搜你想要...")
                //$(".searchMsg").val("搜你想要...").removeClass('reset').parent().removeClass('set');
                $('.resList').find('ul').hide().end().find('div').show();
                ///////////////加载资源类型列表///////////////////
                Current.InitResourceTypeList(Current.CataID,function() {
                    $($('.minNav ul li')[0]).click();
                });
            } else {
                var ResourceType = $($('.minNav ul li.cur')[0]).attr("id");
                var ResourceStyle = $($('.minNav ul li.cur')[0]).attr("parent");

                ///////////////加载资源类型列表///////////////////
                Current.InitResourceTypeList(Current.CataID,function () {
                    var ResourceType = $($('.minNav ul li.cur')[0]).attr("id");
                    var ResourceStyle = $($('.minNav ul li.cur')[0]).attr("parent");
                    Current.InitResourceList(ResourceType, ResourceStyle);
                });
            }
        });
        $('.sidebar>ul>li>span')[0].click();
    };
    //////////////////////////////////////////////////
    ///////////////加载资源类型列表///////////////////
    //////////////////////////////////////////////////
    this.InitResourceTypeList = function (cataID, callback) {
        var parentCataID = 0;
        if (cataID!= "") 
            parentCataID = cataID;
        else
            parentCataID = $(".sidebar li.check").attr("id");
        var sendData = {BookID:Current.BookID,ParentID: parentCataID };
        $.post("/Resource/GetResourceTypeList", sendData, function (data) {
            Current.ResourceList = data.ResourceList;
            if (Current.ResourceList.length!=0) {
                var type_list = "27,10,12,28,5,9,6,2,4,41";
                var list = type_list.split(',');
                var html = "";

                $.each(list, function (n, model) {
                    var result = false;
                    $.each(data.TypeList, function (t, type) {
                        if (type.ID == model) {
                            if (html == "")
                                html += '<li id="' + type.ID + '" parent="' + type.ParentID + '" class="cur">' + type.CodeName + '</li>';
                            else
                                html += '<li id="' + type.ID + '" parent="' + type.ParentID + '">' + type.CodeName + '</li>';
                            result = true;
                            return;
                        }
                    });
                });
                $('.minNav ul').html(html);
                $('.minNav ul').append('<i class="navslip" style="width: 108px;"></i>');
                /*改变滑块宽度、位置*/
                var li = $('.minNav ul li.cur')[0];
                $('.minNav .navslip').stop().animate({
                    width: $(li).width() + 10 + 'px',
                    left: parseInt($(li).position().left) + 'px'
                });
                ///////////////资源类型点击事件///////////////////
                Current.BindTypeClick();
            } else {
                $('.minNav ul').html("");
            }
            callback();
        });
    };
    //////////////////////////////////////////////////
    ///////////////资源类型点击事件///////////////////
    //////////////////////////////////////////////////
    this.BindTypeClick = function () {
        /*课件类型筛选*/
        $('.minNav ul li').unbind('click');
        $('.minNav ul li').bind('click', function () {
            $(this).siblings().removeClass('cur').end().addClass('cur');
            /*改变滑块宽度、位置*/
            $('.minNav .navslip').stop().animate({
                width: $(this).width() + 10 + 'px',
                left: parseInt($(this).position().left) + 'px'
            });

            var ResourceType = $(this).attr("id");
            var ResourceStyle = $($('.minNav ul li.cur')[0]).attr("parent");

            var firstCata = $('.sidebar>ul>li.check');//当前选择的单元
            var childCata = $(firstCata).find('li.cur');//当前选择的单元子目录

            if (childCata.length == 0)
                Current.CataID = $(firstCata[0]).attr("id");
            else
                Current.CataID = $(childCata[0]).attr("id");
            Current.InitResourceList(ResourceType, ResourceStyle);
        });
    };
    //////////////////////////////////////////////////
    ///////////////搜索点击事件///////////////////////
    //////////////////////////////////////////////////
    this.BindSearchClick = function () {
        /*search事件*/
        $('.searchAct').bind('click', function () {
            if ($(this).next().val() === '') {
                var value = $(this).next().val();
                $(".searchM .left").empty();

                $($(".searchM .left")[0]).append('<span><a href="#">学习资源</a></span><em></em><span>' + value + '</span>');
                var bookID = $("#divBookID").attr("name");

                var html = '<li class="cur">' + value + '</li>';
                $(".minNav ul").html(html);
                $(".navslip").css({ "width": $(".minNav").find("li.cur").width() + 10, "left": "0px" });
                Current.InitResourceListBySearch(bookID, value);
            } else {
                if ($(this).next().val() === "搜你想要...") {
                    alert("请输入关键字!");
                    return;
                }
                var value = $(this).next().val();
                $(".searchM .left").empty();

                $($(".searchM .left")[0]).append('<span><a href="#">学习资源</a></span><em></em><span>' + value + '</span>');
                var bookID = $("#divBookID").attr("name");

                var html = '<li class="cur">' + value + '</li>';
                $(".minNav ul").html(html);
                $(".navslip").css({ "width": $(".minNav").find("li.cur").width()+10, "left": "0px" });
                Current.InitResourceListBySearch(bookID, value);
            }
        })
        /*输入关键字*/
        $('.searchMsg').bind('focus', function () {
            //$(this).parent().addClass('set');
            if ($(this).val() === "搜你想要...") {
                $(this).val('')
                //$(this).val('').addClass('reset');
            }
        })
        //    .bind('blur', function () {
        //    //$(this).parent().removeClass('set');
        //    if ($(this).val() === "") {
        //        $(this).val("搜你想要...")
        //        //$(this).val("搜你想要...").removeClass('reset');
        //        $('.resList').find('ul').hide().end().find('div').show();
        //        var cataname = $(".sidebar ul li.check").attr("name");
        //        $(".searchM .left").empty();
        //        $($(".searchM .left")[0]).append('<span><a href="#">学习资源</a></span><em></em><span title="' + cataname + '">' + cataname + '</span>');
        //        ///////////////加载资源类型列表///////////////////
        //        Current.InitResourceTypeList(function() {
        //            $($('.minNav ul li')[0]).click();
        //        });
        //    } else {
        //        //$(this).parent().addClass('set');
        //        var value = $(this).val();
        //        $(".searchM .left").empty();

        //        $($(".searchM .left")[0]).append('<span><a href="#">学习资源</a></span><em></em><span>' + value + '</span>');
        //        var bookID = $("#divBookID").attr("name");

        //        var html = '<li class="cur">' + value + '</li>';
        //        $(".minNav ul").html(html);
        //        $(".navslip").css({ "width": $(".minNav").find("li.cur").width() + 10, "left": "0px" });
        //        Current.InitResourceListBySearch(bookID, value);
        //    }
        //});

        $(".searchMsg").keydown(function (event) {
            if (event.keyCode == 13) {
                $('.searchAct').click();
            }
        });
    };
    //////////////////////////////////////////////////
    ///////////////资源预览事件///////////////////////
    //////////////////////////////////////////////////
    this.BindResourceViewClick = function () {
        /*资源预览*/
        $("input.view").unbind('click');
        $("input.view").bind('click', function () {
            var fileID = $(this).attr("name");
            $.ajax({
                type: "POST",
                url: Current.FileUrl + "Preview.ashx",
                data: { FileID: fileID },
                dataType: "jsonp",
                async: true,
                success: function (response) {
                    ///////////////资源预览///////////
                    Current.ViewResource(response.URL);
                },
                error: function (request, status, error) {

                }
            });
        });

        $(".resList").find("p>input.view").each(function() {
            var image = $(this).parent().parent().find("b>img");
            $(image).unbind('click');
            $(image).bind('click', function() {
                var fileID = $(this).parent().parent().find("input.view").attr("name");
                $.ajax({
                    type: "POST",
                    url: Current.FileUrl + "Preview.ashx",
                    data: { FileID: fileID },
                    dataType: "jsonp",
                    async: true,
                    success: function (response) {
                        ///////////////资源预览///////////
                        Current.ViewResource(response.URL);
                    },
                    error: function (request, status, error) {

                    }
                });
            });
        });
};
//////////////////////////////////////////////////
///////////////资源预览///////////////////////////
//////////////////////////////////////////////////
this.ViewResource = function (url) {
    DialogC.init();
    DialogC.setSkin('skin01');
    DialogC.addCont(dialogFn(url));
};
//////////////////////////////////////////////////
///////////////资源下载事件///////////////////////
//////////////////////////////////////////////////
this.BindResourceDownClick = function () {
    /*资源下载*/
    $("input.download").unbind('click');
    $("input.download").bind('click', function () {
        var fileID = $(this).attr("name");
        var name = $(this).parent().parent().find("label").html();
        var url = Current.FileUrl + "GetFiles.ashx?FileID=" + fileID + "&name=" + escape(name);
        window.open(url);
    });
};

//////////////////////////////////////////////////
///////////////资源列表加载///////////////////////
//////////////////////////////////////////////////
this.InitResourceList = function (resourceType, resourceStyle) {
        var type = 0, style = 0;
        if (resourceStyle != 0) {
            type = resourceStyle;
            style = resourceType;
        } else {
            type = resourceType;
            style = resourceStyle;
        }
        var num = 0;
        var html = "";
        if (Current.ResourceList.length != 0) {
            var arr = [];
            if (Current.ChildIDStr != "")
                arr = Current.ChildIDStr.split(",");
            html += "<ul>";
            $.each(Current.ResourceList, function (r, resource) {
                var result = false;
                if (arr.length!=0) {
                    if (resource.ResourceType == type && resource.ResourceStyle == style && $.inArray(resource.Catalog.toString(), arr) != -1)
                        result = true;
                } else {
                    if (resource.ResourceType == type && resource.ResourceStyle == style)
                        result = true;
                }
                if (result) {
                    num++;
                    html += '<li id="' + resource.ID + '">';
                    html += '<span>';
                    html += '<b><img src="' + Current.FileUrl + 'GetFileImg.ashx?fileID=' + resource.FileID + '&view=true" alt=""></b>';
                    html += '<label title="' + resource.Title + '">' + resource.Title + '</label>';
                    html += '<p>';
                    if (resource.FileType == "jpg" ||
                        resource.FileType == "png" ||
                        resource.FileType == "mp4" ||
                        resource.FileType == "mp3" ||
                        resource.FileType == "swf" ||
                        resource.FileType == "gif" ||
                        resource.FileType == "flv" ||
                        resource.FileType == "wmv" ||
                        resource.FileType == "bmp"
                        ) {
                        html += '<input name="' + resource.FileID + '" class="view" type="button" value="预览">';
                        html += '<input name="' + resource.FileID + '" class="download" type="button" value="下载">';
                    } else {
                        html += '<input name="' + resource.FileID + '" class="download only" type="button" value="下载">';
                    }
                    html += '</p>';
                    html += '</span>';
                    html += '</li>';
                }
            });
            html += '</ul>';
        } else {
            html += '<div class="nothing"></div>';
        }
        $(".resList").html("").html(html);
        $("#txtTotal").html(num);
        $(".nothing").attr("style", "width:100%;height:500px;background:url(../Content/App_Theme/image/nothing_bg.png) center center no-repeat;");

        ///////////////资源预览事件///////////////////
        Current.BindResourceViewClick();
        ///////////////资源下载事件///////////////////
        Current.BindResourceDownClick();
};
//////////////////////////////////////////////
///////////////搜索资源列表///////////////////
//////////////////////////////////////////////
this.InitResourceListBySearch = function (bookID,keyword) {
    var data = { BookID: bookID, KeyWord: keyword };
    $.post("/Resource/GetResourceByKey", data, function (returnData) {
        $("#txtTotal").html(returnData.length);
        var html = "";
        if (returnData.length != 0) {
            html += "<ul>";
            $.each(returnData, function (r, resource) {
                html += '<li id="' + resource.ID + '">';
                html += '<span>';
                html += '<b><img src="' + Current.FileUrl + 'GetFileImg.ashx?fileID=' + resource.FileID + '&view=true" alt=""></b>';
                html += '<label title="' + resource.Title + '">' + resource.Title + '</label>';
                html += '<p>';
                if (resource.FileType == "jpg" ||
                        resource.FileType == "png" ||
                        resource.FileType == "mp4" ||
                        resource.FileType == "mp3" ||
                        resource.FileType == "swf" ||
                        resource.FileType == "gif" ||
                        resource.FileType == "flv" ||
                        resource.FileType == "wmv" ||
                        resource.FileType == "bmp"
                        ) {
                    html += '<input name="' + resource.FileID + '" class="view" type="button" value="预览">';
                    html += '<input name="' + resource.FileID + '" class="download" type="button" value="下载">';
                } else {
                    html += '<input name="' + resource.FileID + '" class="download only" type="button" value="下载">';
                }
                html += '</p>';
                html += '</span>';
                html += '</li>';
            });
            html += '</ul>';
        } else {
            html += '<div class="nothing"></div>';
        }
        $(".resList").html("").html(html);
        $(".nothing").attr("style", "width:100%;height:500px;background:url(../Content/App_Theme/image/nothing_bg.png) center center no-repeat;");

        ///////////////资源预览事件///////////////////
        Current.BindResourceViewClick();
        ///////////////资源下载事件///////////////////
        Current.BindResourceDownClick();
    });
};
};

$(function () {
    var resource = new Resource();
    resource.Init();
});