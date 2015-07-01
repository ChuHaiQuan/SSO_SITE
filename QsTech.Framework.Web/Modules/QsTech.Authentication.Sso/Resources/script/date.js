/*    
 函数：格式化日期    
 参数：formatStr-格式化字符串    
     d：将日显示为不带前导零的数字，如1    
     dd：将日显示为带前导零的数字，如01    
     ddd：将日显示为缩写形式，如Sun    
     dddd：将日显示为全名，如Sunday    
     M：将月份显示为不带前导零的数字，如一月显示为1    
     MM：将月份显示为带前导零的数字，如01   
     MMM：将月份显示为缩写形式，如Jan   
     MMMM：将月份显示为完整月份名，如January   
     yy：以两位数字格式显示年份   
     yyyy：以四位数字格式显示年份   
     h：使用12小时制将小时显示为不带前导零的数字，注意||的用法   
     hh：使用12小时制将小时显示为带前导零的数字   
     H：使用24小时制将小时显示为不带前导零的数字   
     HH：使用24小时制将小时显示为带前导零的数字   
     m：将分钟显示为不带前导零的数字   
     mm：将分钟显示为带前导零的数字   
     s：将秒显示为不带前导零的数字   
     ss：将秒显示为带前导零的数字   
     l：将毫秒显示为不带前导零的数字   
     ll：将毫秒显示为带前导零的数字   
     tt：显示am/pm   
     TT：显示AM/PM   
返回：格式化后的日期   
*/
Date.prototype.format = function(formatStr) {
    var date = this;
    /*   
     函数：填充0字符   
     参数：value-需要填充的字符串, length-总长度   
     返回：填充后的字符串   
     */
    var zeroize = function(value, length) {
        if (!length) {
            length = 2;
        }
        value = new String(value);
        for ( var i = 0, zeros = ''; i < (length - value.length); i++) {
            zeros += '0';
        }
        return zeros + value;
    };
    return formatStr.replace(/"[^"]*"|'[^']*'|\b(?:d{1,4}|M{1,4}|yy(?:yy)?|([hHmstT])\1?|[lLZ])\b/g, function($0) {
        switch ($0) {
            case 'd':
                return date.getDate();
            case 'dd':
                return zeroize(date.getDate());
            case 'ddd':
                return ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'][date.getDay()];
            case 'dddd':
                return ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][date.getDay()];
            case 'M':
                return date.getMonth() + 1;
            case 'MM':
                return zeroize(date.getMonth() + 1);
            case 'MMM':
                return ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'][date.getMonth()];
            case 'MMMM':
                return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'][date.getMonth()];
            case 'yy':
                return new String(date.getFullYear()).substr(2);
            case 'yyyy':
                return date.getFullYear();
            case 'h':
                return date.getHours() % 12 || 12;
            case 'hh':
                return zeroize(date.getHours() % 12 || 12);
            case 'H':
                return date.getHours();
            case 'HH':
                return zeroize(date.getHours());
            case 'm':
                return date.getMinutes();
            case 'mm':
                return zeroize(date.getMinutes());
            case 's':
                return date.getSeconds();
            case 'ss':
                return zeroize(date.getSeconds());
            case 'l':
                return date.getMilliseconds();
            case 'll':
                return zeroize(date.getMilliseconds());
            case 'tt':
                return date.getHours() < 12 ? 'am' : 'pm';
            case 'TT':
                return date.getHours() < 12 ? 'AM' : 'PM';
        }
    });
}
   
/*
 * 日期操作函数
 * 参数：
 *  s: 秒
 *  m: 分
 *  H/h: 时
 *  d: 日
 *  M: 月
 *  y: 年
 */
Date.prototype.add = function(intervalStr, number) {     
    var date = this;
    //计算year年month月的天数
    var dayCount = function(year, month) {  
        if (month == 1) {  
            if (year%4==0 && (year%100!=0 || year%400==0)) {
                return 29;
            }
            else {
                return 28; 
            }
        } else if ((month <= 6 && month % 2 == 0) || (month > 6 && month % 2 == 1)) {
            return 31;
        }
        else {
            return 30;  
        }
    }
    switch (intervalStr) {     
        case 's' :
            return new Date(date.getTime() + (1000 * number));    
        case 'm' :
            return new Date(date.getTime() + (60000 * number));
        case 'H' :
        case 'h' :
            return new Date(date.getTime() + (3600000 * number));    
        case 'd' :
            return new Date(date.getTime() + (86400000 * number));
        case 'M' :
            var year = date.getFullYear() + parseInt(number/12) + parseInt((date.getMonth() + number%12)/12);
            var month = (date.getMonth() + number%12)%12;
            if(month < 0) {
                month += 12;
                year--;
            }
            if(date.getDate() > dayCount(year, month)) {
                return new Date(year, month, dayCount(year, month), date.getHours(), date.getMinutes(), date.getSeconds()); 
            } else {
                return new Date(year, month, date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds()); 
            }
        case 'y' :
            var year = date.getFullYear() + number;
            if(date.getDate() > dayCount(year, date.getMonth())) {
                return new Date(year, date.getMonth(), dayCount(year, date.getMonth()), date.getHours(), date.getMinutes(), date.getSeconds());
            } else {
                return new Date(year, date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds());
            }
    }
    return date;
}    
   
/** 解析日期函数 */
Date.parse = function(dateString, formatString) {
    if(!formatString) {
        formatString = "yyyy/MM/dd";
    }
    /** year : /yyyy/ */
    var _y4 = "([0-9]{4})";
    /** year : /yy/ */
    var _y2 = "([0-9]{2})";
    /** index year */
    var _yi = -1;
    /** month : /MM/ */
    var _M2 = "(0[1-9]|1[0-2])";
    /** month : /M/ */
    var _M1 = "([1-9]|1[0-2])";
    /** index month */
    var _Mi = -1;
    /** day : /dd/ */
    var _d2 = "(0[1-9]|[1-2][0-9]|30|31)";
    /** day : /d/ */
    var _d1 = "([1-9]|[1-2][0-9]|30|31)";
    /** index day */
    var _di = -1;
    /** hour : /HH/ */
    var _H2 = "([0-1][0-9]|20|21|22|23)";
    /** hour : /H/ */
    var _H1 = "([0-9]|1[0-9]|20|21|22|23)";
    /** index hour */
    var _Hi = -1;
    /** minute : /mm/ */
    var _m2 = "([0-5][0-9])";
    /** minute : /m/ */
    var _m1 = "([0-9]|[1-5][0-9])";
    /** index minute */
    var _mi = -1;
    /** second : /ss/ */
    var _s2 = "([0-5][0-9])";
    /** second : /s/ */
    var _s1 = "([0-9]|[1-5][0-9])";
    /** index month */
    var _si = -1;
    var regexp;
    var validateDate = function(dateString, formatString) {
        dateString = dateString.replace(/(^\s*)|(\s*$)/g, "");
        if (dateString == "") {
            return;
        }
        var reg = formatString;
        reg = reg.replace(/yyyy/, _y4);
        reg = reg.replace(/yy/, _y2);
        reg = reg.replace(/MM/, _M2);
        reg = reg.replace(/M/, _M1);
        reg = reg.replace(/dd/, _d2);
        reg = reg.replace(/d/, _d1);
        reg = reg.replace(/HH/, _H2);
        reg = reg.replace(/H/, _H1);
        reg = reg.replace(/mm/, _m2);
        reg = reg.replace(/m/, _m1);
        reg = reg.replace(/ss/, _s2);
        reg = reg.replace(/s/, _s1);
        reg = new RegExp("^" + reg + "$");
        regexp = reg;
        return reg.test(dateString);
    }
    var validateIndex = function(formatString) {
        var ia = new Array();
        var i = 0;
        _yi = formatString.search(/yyyy/);
        if (_yi < 0) {
            _yi = formatString.search(/yy/);
        }
        if (_yi >= 0) {
            ia[i] = _yi;
            i++;
        }
        _Mi = formatString.search(/MM/);
        if (_Mi < 0) {
            _Mi = formatString.search(/M/);
        }
        if (_Mi >= 0) {
            ia[i] = _Mi;
            i++;
        }
        _di = formatString.search(/dd/);
        if (_di < 0) {
            _di = formatString.search(/d/);
        }
        if (_di >= 0) {
            ia[i] = _di;
            i++;
        }
        _Hi = formatString.search(/HH/);
        if (_Hi < 0) {
            _Hi = formatString.search(/H/);
        }
        if (_Hi >= 0) {
            ia[i] = _Hi;
            i++;
        }
        _mi = formatString.search(/mm/);
        if (_mi < 0) {
            _mi = formatString.search(/m/);
        }
        if (_mi >= 0) {
            ia[i] = _mi;
            i++;
        }
        _si = formatString.search(/ss/);
        if (_si < 0) {
            _si = formatString.search(/s/);
        }
        if (_si >= 0) {
            ia[i] = _si;
            i++;
        }
        var ia2 = new Array(_yi, _Mi, _di, _Hi, _mi, _si);
        for (i = 0; i < ia.length - 1; i++) {
            for (j = 0; j < ia.length - 1 - i; j++) {
                if (ia[j] > ia[j + 1]) {
                    temp = ia[j];
                    ia[j] = ia[j + 1];
                    ia[j + 1] = temp;
                }
            }
        }
        for (i = 0; i < ia.length; i++) {
            for (j = 0; j < ia2.length; j++) {
                if (ia[i] == ia2[j]) {
                    ia2[j] = i;
                }
            }
        }
        return ia2;
    }
    if (validateDate(dateString, formatString)) {
        var now = new Date();
        var vals = regexp.exec(dateString);
        var index = validateIndex(formatString);
        var year = index[0] >= 0 ? vals[index[0] + 1] : now.getFullYear();
        var month = index[1] >= 0 ? (vals[index[1] + 1] - 1) : now.getMonth();
        var day = index[2] >= 0 ? vals[index[2] + 1] : now.getDate();
        var hour = index[3] >= 0 ? vals[index[3] + 1] : "";
        var minute = index[4] >= 0 ? vals[index[4] + 1] : "";
        var second = index[5] >= 0 ? vals[index[5] + 1] : "";
        var validate;
        if (hour == "") {
            validate = new Date(year, month, day);
        } else {
            validate = new Date(year, month, day, hour, minute, second);
        }
        if (validate.getDate() == day) {
            return validate;
        }
    }
    return null;
}