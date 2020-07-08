//Date的扩展

(function () {
    /**
  * 对Date的扩展，将 Date 转化为指定格式的String 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q)
  * 可以用 1-2 个占位符 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) eg: (new
  * Date()).format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 (new
  * Date()).format("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04 (new
  * Date()).format("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04 (new
  * Date()).format("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04 (new
  * Date()).format("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18
  */
    Date.prototype.format = function (fmt) {
        var o = {
            "Y+": this.getFullYear(),
            "M+": this.getMonth() + 1,
            // 月份
            "d+": this.getDate(),
            // 日
            "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12,
            // 小时
            "H+": this.getHours(),
            // 小时
            "m+": this.getMinutes(),
            // 分
            "s+": this.getSeconds(),
            // 秒
            "q+": Math.floor((this.getMonth() + 3) / 3),
            // 季度
            "S": this.getMilliseconds()
            // 毫秒
        };
        var week = {
            "0": "/u65e5",
            "1": "/u4e00",
            "2": "/u4e8c",
            "3": "/u4e09",
            "4": "/u56db",
            "5": "/u4e94",
            "6": "/u516d"
        };
        if (/(y+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "")
                .substr(4 - RegExp.$1.length));
        }
        if (/(E+)/.test(fmt)) {
            fmt = fmt
                .replace(
                    RegExp.$1,
                    ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f"
                        : "/u5468")
                        : "")
                    + week[this.getDay() + ""]);
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(fmt)) {
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k])
                    : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    };

    //+-------------------------------------------------  
    //  strInterval  
    //  s :秒 ;   m :分钟 ;  h :小时 ;
    //  d :天 ;   w :周  ;   q :季度;
    //  M :月份       y :年;
    //  Number  变化的数值  可以是正数 也可以是负数
    //+---------------------------------------------------  
    Date.prototype.add = function (strInterval, Number) {
        var dtTmp = this;
        switch (strInterval) {
            case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));
            case 'm': return new Date(Date.parse(dtTmp) + (60000 * Number));
            case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));
            case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));
            case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
            case 'q': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
            case 'M': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
            case 'y': return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        }
    }

})();

//Jquery
(function () {
    //beforeSend 返回false则中断请求
    //error 返回true 表示已经在方法里处理了异常.返回false表示没有处理
    //默认为Post异步
    $.cutPost = function (url, paras, type, success, error, complete, beforeSend, async) {
        if (async == null) {
            async = true;
        }
        if (!type) {
            type = 'POST'
        }

        $.ajax({
            url: url,
            async: async,
            type: type,
            data: paras,
            beforeSend: function (XHR) {

                if (beforeSend) {
                    n = beforeSend(XHR);
                    if (n != null) {
                        return n;
                    }
                }

            },
            complete: function (XHR, TS) {
                if (complete)
                    complete(XHR, TS);
            },

            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var n = false;
                if (error)
                    n = error();
                //方法内未处理异常则在这里提示
                if (!n) {
                    if (window.$Vue) {
                        $Vue.$Message.error((textStatus || errorThrown));
                    } else {
                        alert((textStatus || errorThrown));
                    }
                }

            },
            success: function (data, textStatus, jqXHR) {
                if (success) {
                    success(data, textStatus, jqXHR);
                }
            }
        });
    }

    //将页面上的table导成一个excel
    $.tableExportXls = function (table, name) {
        var uri = 'data:application/vnd.ms-excel;base64,',
            template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head> <meta http-equiv=\'Content-Type\' content=\'application/vnd.ms-excel; charset=utf-8\' /><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table border="1">{table}</table></body></html>',
            base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) },
            format = function (s, c) {
                return s.replace(/{(\w+)}/g,
                        function (m, p) { return c[p]; })
            }
        if (!table.nodeType) table = document.getElementById(table)

        var trs = $(table).find("tr");
        if (trs.length > 5) {
            trs.remove(':gt(' + (trs.length - 6) + ')')
        }
        var ctx = { worksheet: 'sheet1', table: table.innerHTML }
        var a = document.createElement("a");
        a.download = name;
        a.href = uri + base64(format(template, ctx));
        a.target = "_blank";
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
    }
})();