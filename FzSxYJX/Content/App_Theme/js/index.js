/**
 * Created by tyx on 2017-12-25.
 */

//页面数据
var pageList={
    "grade":["小学","初中"],
    "subject":[{
        "subList":["语文","数学","外语"]
    },{
        "subList":["语文","数学","外语","物理","生物","化学","政治","历史","地理"]
    }]
}


$(function(){
    //初始化
    var htmlH = '';
    var subHtml = '';
    var _grade = pageList.grade;
    var _subject = pageList.subject;
    //遍历grade结构
    for(var i = 0;i<_grade.length;i++){
        if(i === 0){
            htmlH += '<li class="cur" indexVal='+i+'>'+_grade[i]+'</li>';
        }else{
            htmlH += '<li indexVal='+i+'>'+_grade[i]+'</li>';
        }
    }
    $('#gradeList').html(htmlH);

    var _subList = _subject[0].subList;
    //遍历subList结构
    for(var i = 0;i<_subList.length;i++){
        //subHtml += '<li>'+_subList[i]+'</li>';
        subHtml += '<li subject="'+_subList[i]+'"><img src="./img/subject/subject_'+(i+1)+'.png"/></li>';
    }
    $('#subjectList').html(subHtml);
    //绑定年段选择事件
    $('#gradeList li').bind('click',changeGrage);
    //给学科绑定点击事件
    $('#subjectList li').bind('click',function(){
        alert($(this).attr('subject'));
    })

    gotoTop();
});

//年段选择
function changeGrage(){
    if($(this).attr('class')==='cur'){return;}
    var subHtml = '';
    var _subject = pageList.subject;
    var indexVal = $(this).attr("indexVal");
    //点击效果
    $(this).siblings().removeClass('cur').end().addClass('cur');
    var _subject = _subject[indexVal].subList;
    //遍历subList结构
    for(var i = 0;i<_subject.length;i++){
        //subHtml += '<li>'+_subject[i]+'</li>';
        subHtml += '<li subject="'+_subject[i]+'"><img src="./img/subject/subject_'+(i+1)+'.png"/></li>';
    }
    $('#subjectList').html(subHtml);
    //给学科绑定点击事件
    $('#subjectList li').bind('click',function(){
        alert($(this).attr('subject'));
    })
}