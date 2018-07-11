$(function () {
    //绑定年段选择事件
    $('#gradeList li').bind('click', changeGrage);
    gotoTop();
    var ua = navigator.userAgent.toLowerCase();
    var isIE = ua.indexOf("msie") > -1;
    // var safariVersion;
    if (isIE) {
        if (document.readyState != "loading") {
            setWrap();
        }
    } else {
        window.onload = function () {
            setWrap();
        }
    }

})
//年段选择
function changeGrage() {
    if ($(this).attr('class') === 'cur') { return; }
    var subHtml = '';
    //点击效果
    $(this).siblings().removeClass('cur').end().addClass('cur');
    var stageid = $("#gradeList .cur").attr("id");
    $.ajax({
        type: 'post',
        url: '/Home/GetSubjectListByStageID',
        data: { stageID: stageid },
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data) {
                if (data.Success) {
                    //var result = eval('(' + data.Data + ')');
                    //遍历subList结构
                    for (var i = 0; i < data.Data.length; i++) {
                        subHtml += '<li subject="' + data.Data[i].SubjectName + '"><a href="./Apply/Index?subjectId=' + data.Data[i].SubjectID + '&stageId=' + stageid + '"><img src="./Content/App_Theme/image/subject/subject_' + (data.Data[i].SubjectID) + '.png"/></a></li>';
                    }
                    $('#subjectList').html(subHtml);
                } else {
                    $('#subjectList').html('<p>暂无学科</p>');
                }
            } else {
                $('#subjectList').html('<p>暂无学科</p>');
            }
            setWrap();
        }         
    });  
}