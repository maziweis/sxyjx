/**
 * Created by tyx on 2017-12-25.
 */

var bookData={
    "version":["人教版","北京版","牛津版","北师大版","牛津深圳版"],
    "bookMsg":[{
        "version":"人教版",
        "bookList":[{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        },{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        },{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        },{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        },{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        },{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        },{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        }]
    },{
        "version":"北京版",
        "bookList":[{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        }]
    },{
        "version":"牛津版",
        "bookList":[{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        },{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        }]
    },{
        "version":"北师大版",
        "bookList":[{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        },{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        }]
    },{
        "version":"牛津深圳版",
        "bookList":[{
            "bookName":"七年级数学·上","bookFace":"./img/cover.png"
        }]
    }
    ]
}




$(function(){
    //初始化
    //获取url参数
    var _grade = decodeURI(request('greadNum'));
    var _subject = decodeURI(request('subject'));
    console.log("所选年段："+_grade+"\n学科："+_subject);
    //组装年段
    var htmlH = '';
    htmlH += '<li class="cur">'+_grade+'</li>';
    $('#gradeList').html(htmlH);
    //组装版本筛选
    var vHtml = '';
    var _versions = bookData.version;
    for(var i = 0 ; i < _versions.length; i++){
        if(i===0){
            vHtml += '<li class="cur">'+_versions[i]+'</li>';
        }else{
            vHtml += '<li>'+_versions[i]+'</li>';
        }
    }
    $('#versionList').html(vHtml);
    //绑定版本筛选
    $('.selectC ul li').bind('click',ChooseVersions);

    //组装book结构
    var bookHtml = '';
    var verNum = $('#versionList li.cur').index();
    var _bookMsg = bookData.bookMsg[verNum];
    var _bookList = _bookMsg.bookList;
    for(var i = 0; i < _bookList.length; i++){
        bookHtml += '<li><a href="#"><em class="pen"></em><img src=' + _bookList[i].bookFace + ' alt="cover"><span>' + _bookList[i].bookName + '</span></a></li>'
    }
    $('#bookList').html(bookHtml);
    gotoTop();
})

//筛选版本
function ChooseVersions(){
    $(this).siblings().removeClass('cur').end().addClass('cur');
    //重新加载结构
    var bookHtml = '';
    var verNum = $(this).index();
    var _bookMsg = bookData.bookMsg[verNum];
    var _bookList = _bookMsg.bookList;
    for(var i = 0; i < _bookList.length; i++){
        bookHtml += '<li><a href="#"><em class="pen"></em><img src=' + _bookList[i].bookFace + ' alt="cover"><span>' + _bookList[i].bookName + '</span></a></li>'
    }
    $('#bookList').html(bookHtml);

}