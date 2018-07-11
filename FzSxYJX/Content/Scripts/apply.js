$(function () {

    //绑定年段选择事件
    $('#gradeList li').bind('click', changeGrage);
    //绑定版本筛选
    $('.selectC ul li').bind('click', ChooseVersions);
    /*初始化滑块宽度*/
    $('.navslip').width($('#versionList li.cur').width());
    var html = '';
    html +="<div class='return' id='return' title='返回上一页'>";
    html += "<a href='javascript:void(0)'></a></div>";
 //   $(".actionDiv").html(html);
    gotoTop();
    goback()
    BindClick();
    setWrap();

   
})
/*返回上一页*/
function goback() {
   // var sid = $("#gradeList .cur").attr("id");
    $('#return').bind('click', function () {
         window.location = '/';
       // window.history.go(-1);
    })
}

//年段选择
function changeGrage() {
    if ($(this).attr('class') === 'cur') { return; }
    var courseHtml = '';
    var editionHtml = '';
    var moviceHtml = '';
    //点击效果
    $(this).siblings().removeClass('cur').end().addClass('cur');
    var stageid = $("#stageID").val();
    var subid = $("#subjectID").val();
    $.ajax({
        type: 'post',
        url: '/Apply/GetBookListAndMovice',
        data: { subjectId: subid, stageId: stageid},
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data) {
                if (data.Success) {
                    //加载教材
                    if (data.Data.CourseAllList.EditionList && data.Data.CourseAllList.EditionList.length > 0) {
                        for (var j = 0; j < data.Data.CourseAllList.EditionList.length; j++) {
                            if (0 == j) {
                                editionHtml += '<li class="cur" id="' + data.Data.CourseAllList.EditionList[j].EditionID + '">' + data.Data.CourseAllList.EditionList[j].EditionName + '</li>';
                            } else {
                                editionHtml += '<li id="' + data.Data.CourseAllList.EditionList[j].EditionID + '">' + data.Data.CourseAllList.EditionList[j].EditionName + '</li>';
                            }
                        }
                        for (var k = 0; k < data.Data.CourseAllList.CourseList.length; k++) {
                            if (data.Data.CourseAllList.CourseList[k].IsOwnApply) {
                                courseHtml += '<li class="OwnApply" id="' + data.Data.CourseAllList.CourseList[k].ID + '"><a><b><em class="pen"></em><img src="' + data.Data.CourseAllList.CourseList[k].ImageUrl + '" alt="cover"></b><span>' + data.Data.CourseAllList.CourseList[k].CourseName + '</span></a></li>';
                            } else {
                                courseHtml += '<li id="' + data.Data.CourseAllList.CourseList[k].ID + '"><a href="/Resource/Index?BookID=' + data.Data.CourseAllList.CourseList[k].ID + '" target="_blank"><b><em class="pen"></em><img src="' + data.Data.CourseAllList.CourseList[k].ImageUrl + '" alt="cover"></b><span>' + data.Data.CourseAllList.CourseList[k].CourseName + '</span></a></li>';
                            }
                        }
                    } else {
                        courseHtml += '<li>教材数据正在制作中。。。</li>';
                    }
                   ////加载电影课
                    if (data.Data.MoviceList && data.Data.MoviceList.length > 0) {
                        moviceHtml += '<div class="selectC">';
                        moviceHtml += '<ul>';
                        moviceHtml += '<li class="cur">电影课</li>';
                        moviceHtml += '</ul>';
                        moviceHtml += '<i class="navslip" style="width:61px"></i>';
                        moviceHtml += '</div>';
                        moviceHtml += '<div class="objList">';
                        moviceHtml += '<ul>';
                        for (var i = 0; i < data.Data.MoviceList.length; i++) {
                            moviceHtml += '<li><a href="' + data.Data.MoviceList[i].Url + '" target="_blank">';
                            moviceHtml += '<b><em class="pen"></em><img src="' + data.Data.MoviceList[i].ImageUrl + '" alt="cover"></b>';
                            moviceHtml += '<span>' + data.Data.MoviceList[i].MoviceName + '</span></a></li>';
                        }
                        moviceHtml += '</ul>';
                        moviceHtml += '</div>';
                    }
                    $("#versionList").html(editionHtml);
                    $("#bookList").html(courseHtml);
                    $(".mCont .md02").html(moviceHtml);
                    BindClick();
                    //绑定版本筛选
                    $('.selectC ul li').bind('click', ChooseVersions);     
                    /*改变滑块宽度、位置*/
                    if (editionHtml) {
                        $('#versionList .cur').parent().next().stop().animate({
                            width: $('#versionList .cur').width() + 'px',
                            left: parseInt($('#versionList .cur').position().left) + 'px'
                        })
                    } else {
                        $('#versionList').next().css("width", "0px");
                    }
                   
                } else {
                    $('#subjectList').html('<p>暂无数据</p>');
                }
            } else {
                $('#subjectList').html('<p>暂无数据</p>');
            }
        }
    });
    setWrap();
}

//筛选版本
function ChooseVersions() {
    $(this).siblings().removeClass('cur').end().addClass('cur');
    /*改变滑块宽度、位置*/
    $(this).parent().next().stop().animate({
        width: $(this).width() + 'px',
        left: parseInt($(this).position().left) + 'px'
    })
    //重新加载结构
    var bookHtml = '';
    var ediID = $("#versionList .cur").attr("id");
    var stageid = $("#stageID").val();
    var subid = $("#subjectID").val();
    $.ajax({
        type: 'post',
        url: '/Apply/GetBookListByEditionID',
        data: { ediID: ediID, subjectId: subid, stageId: stageid },
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data) {
                if (data.Success) {
                    if (data.Data && data.Data.length > 0) {
                        for (var i = 0; i < data.Data.length; i++) {
                            if (data.Data[i].IsOwnApply) {
                                bookHtml += '<li class="OwnApply" id="' + data.Data[i].ID + '"><a><b><em class="pen"></em><img src="' + data.Data[i].ImageUrl + '" alt="cover"></b><span>' + data.Data[i].CourseName + '</span></a></li>';
                            }
                            else {
                                bookHtml += '<li id="' + data.Data[i].ID + '"><a href="./Resource/Index?BookID=' + data.Data[i].ID + '" target="_blank"><b><em class="pen"></em><img src="' + data.Data[i].ImageUrl + '" alt="cover"></b><span>' + data.Data[i].CourseName + '</span></a></li>';
                            }
                        }
                    }
                }
            } else {
                bookHtml += '<li>暂无数据</li>'
            }
            $('#bookList').html(bookHtml);
            BindClick();
            setWrap();
        }
    });

}

function BindClick() {
    ///////////云课堂弹出窗口///////////////
    $(".OwnApply").unbind("click");
    $(".OwnApply").click(function () {
        var id = $(this).attr("id");
        ClassDialog(id);
    });
}


function ClassDialog(index) {
    $.post("/Apply/GetCourseAppList", { CourseID: index }, function (data) {
        var html = "";
        var course = data.Data;
        //html += "<div class='headDiv'>";
        //html += "</div>";
        //html += '<div class="bodyDiv"><a onclick=OpenCourse("' + course.BookUrl + '")><img id="coverImg" src="' + course.ImgUrl + '"  /><b>数字课堂</b></a></div>';
        //html += "<div class='footDiv'>";
        //html += " <div class='centerLink'>";
        //html += "<ul>";
        //$.each(course.appList, function (i, item) {
        //    path = item.Url;
        //    switch (item.AppType) {
        //        case 1:
        //            appClass = 'li0'; break;
        //        case 2:
        //            appClass = 'li9'; break;
        //        case 3:
        //            appClass = 'li6'; break;
        //        case 4:
        //            appClass = 'li13'; break;
        //        case 5:
        //            appClass = 'li7'; break;
        //        case 6:
        //            appClass = 'li5'; break;
        //        case 7:
        //            appClass = 'li4'; break;
        //        case 8:
        //            appClass = 'li3'; break;
        //        case 9:
        //            appClass = 'li8'; break;
        //        case 10:
        //            appClass = 'li12'; break;
        //        case 11:
        //            appClass = 'li11'; break;
        //        case 12:
        //            appClass = 'li10'; break;
        //        default:
        //            appClass = 'li1';
        //    }
        //    if (item.AppType == 0)
        //        html += '<li id="practice" ><a href="' + path + '">';
        //    else
        //        html += '<li id="practice" ><a onclick=OpenCourse("' + path + '")>';
        //    html += '<span class="' + appClass + '" ></span><em>' + item.AppName + '</em></a></li>';
        //});
        //html += "</ul>";
        //html += " </div> ";
        //html += "</div>";
        DialogC.init();
        DialogC.addCont(buildHtml(course));
    });
}
//////////打开应用///////////
function OpenCourse(path) {
    $.post("/Apply/CourseAppTransfer", { Url:path}, function (data) {
        if (data.code == 0)
            window.open(data.msg);
        else
            alert(data.msg);
    });
};