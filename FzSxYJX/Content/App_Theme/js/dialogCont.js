/**
 * Created by tyx on 2017-12-29.
 */

; (function () {
    var dialogC = {
        init: function () {   //初始化弹窗结构
            var _this = this;
            var html = '';
            html += '<div class="dialog">';
            html += '   <div class="mask"></div>';
            html += '   <div class="dialogBg">';
            html += '   <table>\n' +
            '                <tr class="t">\n' +
            '                    <td class="l"></td>\n' +
            '                    <td class="c"></td>\n' +
            '                    <td class="r"></td>\n' +
            '                </tr>\n' +
            '                <tr class="m">\n' +
            '                    <td class="l"></td>\n' +
            '                    <td class="c">' +
            '                       <div class="diaCont">\n' +
            '                           <div class="close"></div>\n' +
            '                           <img class="bgImg" src="/Content/App_Theme/image/dialogCont_bg.png"/>\n' +
            '                           <div class="minCont"></div>\n' +
            '                           </div>\n' +
            '                       </div>\n' +
            '                    </td>\n' +
            '                    <td class="r"></td>\n' +
            '                </tr>\n' +
            '                <tr class="b">\n' +
            '                    <td class="l"></td>\n' +
            '                    <td class="c"></td>\n' +
            '                    <td class="r"></td>\n' +
            '                </tr>\n' +
            '            </table>';
            html += '   </div>';
            html += '</div>';
            $('body').append(html);
            _this.close();
            _this.setSkin();
        },
        //添加内容
        addCont: function (data) {
            $('.dialog .minCont').html(data);
        },
        //设置宽高
        setDialog: function (dialogW, dialogH) {
            var w, h, W, H, X, Y, ratio, bili;
            bili = 0.8;
            if ($('.dialogBg').hasClass('skin01')) {
                ratio = 888 / 680;    //尺寸宽高比
            } else {
                ratio = 949 / 604     //booklist弹窗比例
            }
            W = $(window).width();
            H = $(window).height();
            if (H < 768) {
                dialogH = H - 20;
                dialogW = dialogH * ratio;
            } else {
                dialogH = H * bili;
                dialogW = dialogH * ratio;
            }
            $('.dialogBg').width(dialogW);
            $('.dialogBg').height(dialogH);
            /*默认弹窗大小*/
            if (dialogW && dialogH) {
                w = parseInt(dialogW);
                h = parseInt(dialogH);
            } else {
                w = $('.dialogBg').width();
                h = $('.dialogBg').height();
            }
            $('.diaCont').width(w - 26 * 2)
            $('.diaCont').height(h - 26 * 2)
            //console.log("w:"+w+",h:"+h+",W:"+W+",H:"+H);
            X = (W - w) / 2;
            Y = (H - h) / 2 + 20;
            $('.dialogBg').css({ "top": Y, "left": X/*,"width":w,"height":h*/ });
            $(window).unbind('resize'); //防止重复绑定
            $(window).bind('resize', function () {
                if ($('.dialogBg').is(':visible')) {
                    W = $(window).width();
                    H = $(window).height();
                    if (H < 768) {
                        dialogH = H - 40;
                        dialogW = dialogH * ratio;
                    } else {
                        dialogH = H * bili;
                        dialogW = dialogH * ratio;
                    }
                    $('.dialogBg').width(dialogW);
                    $('.dialogBg').height(dialogH);
                    /*默认弹窗大小*/
                    if (dialogW && dialogH) {
                        w = parseInt(dialogW);
                        h = parseInt(dialogH);
                    } else {
                        //w = 998;
                        //h = 698;
                        w = $('.dialogBg').width();
                        h = $('.dialogBg').height();
                    }
                    $('.diaCont').width(w - 26 * 2)
                    $('.diaCont').height(h - 26 * 2)
                    X = (W - w) / 2;
                    Y = (H - h) / 2 + 20;
                    $('.dialogBg').css({ "top": Y, "left": X });
                }
            })
        },
        setSkin: function (skin) {
            if (skin) {
                $('.dialogBg').addClass(skin);
            }
            this.setDialog();
        },
        /*关闭弹窗*/
        close: function () {
            $('.dialog .close').bind('click', function () {
                //console.log('123')
                $(this).next().html('');
                $('.dialog').fadeOut(500);
                $('.dialog').remove();  //移除弹窗结构
                $(window).unbind('resize'); //防止重复绑定
            })
        }
    }

    this.DialogC = dialogC;
    return this;
})();

//模拟数据
//var data = {
//    "bookFaceURL": "./img/Course.gif",
//    "h4": "配套教材",
//    "hrefList": [
//        { "text": "链接一", "url": "http://www.baidu.com" },
//        { "text": "链接二", "url": "http://www.baidu.com" },
//        { "text": "链接三", "url": "./resourceList.html" }
//    ]
//}
//根据模拟数据组装结构
function buildHtml(data) {
    var _coverUrl = data.ImageUrl;
    var _title = "配套教材";
    var _hrefList = data.BookUrl;
    var htmlS = '';
    htmlS += '<div class="leftC"><i class="iS">数字教材</i>';
    htmlS += '  <span class="bookC"><a href=' + _hrefList + ' target="_blank"><img src=' + _coverUrl + ' alt="封面"></a></span>';
    htmlS += '</div>';
    htmlS += '<div class="rightC">';
    htmlS += '  <div class="wp">';
    htmlS += '      <h4>' + _title + '</h4>';
    htmlS += '      <ul>';



    for (var i = 0; i < data.appList.length; i++) {
      //  if (data.appList[i].AppType == 0) {
       //     htmlS += '<li><a href=' + data.appList[i].Url + '>' + data.appList[i].AppName + '</a></li>';
      //  }else{
            htmlS += '<li><a href=' + data.appList[i].Url + ' target="_blank">' + data.appList[i].AppName + '</a></li>';
      //  }
       
    }
    htmlS += '      <ul>';
    htmlS += '  </div>';
    htmlS += '</div>';
    return htmlS;
}


//模拟数据组转资源预览结构
function dialogFn(viewUrl) {
    var iframeH = '';
    iframeH += '<iframe src="' + viewUrl + '" allowfullscreen="true" weblitallowfullscreen="true" mozallowfullscreen="true" frameborder="0"></iframe>';
    return iframeH;
}
//监控esc关闭窗体
$(document).keydown(function (event) {
    if (event.keyCode === 27) {
        event.preventDefault();
        $('.dialog .close').click();
    }
})