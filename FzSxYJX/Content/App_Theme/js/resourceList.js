/**
 * Created by tyx on 2017-12-26.
 */

var sidebarData = {
    "menuList": [
        { "menuName": "Unit1 I can sing" },
        { "menuName": "Unit1 飞I can sing2", "childCont": ["学习练习-01", "学习练习-02"] },
        { "menuName": "单元三" },
        { "menuName": "单元21s 22 四 授搜索2d sfadaf 课 123顺口溜", "childCont": ["学习练习-01", "学习练习-02"] },
        { "menuName": "单元五" }
    ]
}

$(function () {
    //初始化
    var mHtml = '';
    var _menuList = sidebarData.menuList;
    for (var i = 0; i < _menuList.length; i++) {
        if (i === 0) {
            mHtml += '<li class="cur check"><span title="' + _menuList[i].menuName + '">' + cutStr(_menuList[i].menuName, strNum(_menuList[i].menuName, 18), 18);
        } else {
            mHtml += '<li><span title="' + _menuList[i].menuName + '">' + cutStr(_menuList[i].menuName, strNum(_menuList[i].menuName, 18), 18);
        }
        //判断是否有二级菜单
        if (_menuList[i].childCont && _menuList[i].childCont.length > 0) {
            mHtml += '<em class="em"></em></span>'
            var _childCont = _menuList[i].childCont;
            mHtml += '<ul>'
            for (var n = 0; n < _childCont.length; n++) {
                mHtml += '  <li title="' + _childCont[n] + '">' + _childCont[n] + '</li>'
            }
            mHtml += '</ul>'
        } else {
            mHtml += '<em></em></span>'
        }
    }
    mHtml += '</li>';
    $('.sidebar ul').html(mHtml);

    /*初始化滑块宽度*/
    $('.minNav .navslip').width($('.minNav ul li.cur').width() + 10);

    /*二级菜单
    一级点击事件*/
    $('.sidebar>ul>li>span').bind('click', function () {
        if ($(this).parent().hasClass('cur')) {
            $(this).parent().removeClass('cur');
        } else {
            $(this).parent().find('li').removeClass('cur');
            $(this).parent().siblings().removeClass('cur').removeClass('check').end().addClass('cur').addClass('check');
        }
    })
    /*二级点击事件*/
    $('.sidebar>ul>li>ul>li').bind('click', function () {
        $(this).siblings().removeClass('cur').end().addClass('cur');
    })

    /*课件类型筛选*/
    $('.minNav ul li').bind('click', function () {
        $(this).siblings().removeClass('cur').end().addClass('cur');
        /*改变滑块宽度、位置*/
        $('.minNav .navslip').stop().animate({
            width: $(this).width() + 10 + 'px',
            left: parseInt($(this).position().left) + 'px'
        })
    })

    /*search事件*/
    $('.searchAct').bind('click', function () {
        if ($(this).next().val() === '') {
            alert("请输入关键字!");
        } else {
            if ($(this).next().val() === "搜你想要...") {
                alert("请输入关键字!");
                return;
            }
            alert("搜索关键字：" + $(this).next().val());
        }
    })
    /*输入关键字*/
    $('.searchMsg').bind('focus', function () {
        // $(this).parent().addClass('set');
        if ($(this).val() === "搜你想要...") {
            $(this).val('');
            // $(this).val('').addClass('reset');
        }
    }).bind('blur', function () {
        // $(this).parent().removeClass('set');
        if ($(this).val() === "") {
            $(this).val("搜你想要...");
            // $(this).val("搜你想要...").removeClass('reset');
            $('.resList').find('ul').hide().end().find('div').show();

        } else {
            // $(this).parent().addClass('set');
            alert("提交搜索信息！");
        }
    })

    /*资源预览*/
    $("input.view").bind('click', function () {
        //alert("预览");
        DialogC.init();
        DialogC.setSkin('skin01');
        DialogC.addCont(dialogFn());
    })
    /*资源下载*/
    $("input.download").bind('click', function () {
        alert("下载");
    })

    /*返回上一页*/
    $('#return').bind('click', function () {
        window.history.go(-1);
    })

    gotoTop();
})