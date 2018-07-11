/**
 * Created by tyx on 2017-12-25.
 */

// 通用函数
/*--获取网页传递的参数：
 如获取Default.aspx?ID=123这个URL中ID的值时，调用方法：request("ID")
 --*/
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}
//根据传的字符串获取其字数，一个中文为两个字节，英文为一个字节
function strNum(strTemp,numb) {
    var sum = 0;
    var zyStyle;
    var chineseSum = 0;
    var englishSum = 0;
    var newStr = '';
    for (var i = 0; i < strTemp.length; i++) {
        if ((strTemp.charCodeAt(i) >= 0) && (strTemp.charCodeAt(i) <= 255)) {//英文
            zyStyle = 0;
            sum = sum + 1;
            englishSum++;
        } else {//中文
            zyStyle = 1;
            sum = sum + 2;
            chineseSum++;
        }
        if (sum <= numb) {
            newStr += strTemp.substring(i,i+1);
        }
    }
    var dataR = {
        "sum": sum,
        "chineseSum": chineseSum,
        "englishSum": englishSum,
        "newStr": newStr
    }
    return dataR;
}

function cutStr(str, data,numb) {
    var _str = str;
    var _data = data;
    var returnVal = '';
    if (_data.chineseSum * 2 + _data.englishSum <= numb) {
        returnVal = _str;
    } else {
        var newStrV = _data.newStr;
        returnVal = newStrV.slice(0, -3) + '...';
    }
    return returnVal;
}
function setWrap() {
    if ($('body').height() <= $(window).height()) {
        $('.footer').css({ 'position': 'absolute' });
    } else {
        $('.footer').css({ 'position': 'relative' });
    }
}