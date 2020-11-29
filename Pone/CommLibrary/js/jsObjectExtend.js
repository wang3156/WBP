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
    $.cutPost = function (url, paras, success, error, complete, beforeSend, type, async) {
        if (async == null) {
            async = true;
        }
        if (!type) {
            type = 'POST';
        }
        $.ajax({
            url: url,
            async: async,
            type: type,
            data: JSON.stringify(paras),
            contentType: 'application/json',
            dataType: 'json',
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
                        $Vue.$message.error((textStatus || errorThrown));
                    } else {
                        alert((textStatus || errorThrown));
                    }
                }

            },
            success: function (data, textStatus, jqXHR) {
                if (data.Success) {
                    if (success) {
                        success(data, textStatus, jqXHR);
                    }
                } else {
                    if (window.$Vue) {
                        $Vue.$message.error(data.Msg);
                    } else {
                        alert(data.Msg);
                    }
                }

            }
        });
    }
    //将file 控件中选择的文件转成Base64    
    //回调参数 [{fileName:'',base64:''}]
    $.fileToBase64 = function (domFile, callback, keepbase) {
        var fnum = 0;
        if (!domFile.files || (fnum = domFile.files.length) == 0) {
            alert("没有需要加载的文件!");
            return;
        }
        var reader = new FileReader();
        var b64 = [];
        reader.onerror = function (e) {
            alert("读取文件出错:" + reader.error);
            reader.abort();
        }
        var f, j = 0;
        var re = function () {
            f = domFile.files[j];
            b64[b64.length] = { fileName: f.name };
            reader.readAsDataURL(f);
        }

        reader.onload = function (e) {
            b64[b64.length - 1].base64 = keepbase ? e.target.result : e.target.result.replace(/.+;base64,/, '');
            j++;
            if (j < fnum) {
                setTimeout(re, 1 * 1000);
            }
        }
        re();



        var ti = setInterval(function () {
            if (j == fnum) {
                clearInterval(ti);
                reader.abort();
                if (callback) {
                    callback(b64);
                }
            }
        }, 1000);

    }

    $.downloadBase64File = function (base64Data, fileName, contentType) {
        var byteCharacters = atob(base64Data);
        var byteNumbers = new Array(byteCharacters.length);
        for (var i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        if (!contentType) {
            var extLastIndexOf = fileName.lastIndexOf('.');
            var ext = fileName.substring(extLastIndexOf + 1, fileName.length);//后缀名
            switch (ext.toLowerCase()) {
                case 'jpeg':
                case 'jpg':
                    contentType = 'image/jpeg';
                    break;
                case 'gif':
                    contentType = 'image/gif';
                    break;
                case 'png':
                    contentType = 'image/png';
                    break;
                case 'xls':
                case 'xlm':
                    contentType = 'application/vnd.ms-excel';
                    break;
                case 'xlsx':
                    contentType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
                    break;
                case 'doc':
                    contentType = 'application/msword';
                    break;
                case 'docx':
                    contentType = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document';
                    break;
                case 'pdf':
                    contentType = 'application/pdf';
                    break;
                case 'txt':
                    contentType = 'text/plain';
                    break;
                case 'zip':
                    contentType = 'application/zip';
                    break;
                default:
                    contentType = 'application/octet-stream';
                    break;
            }
        }
        var byteArray = new Uint8Array(byteNumbers);
        var blob = new Blob([byteArray], { type: contentType });
        if (navigator.msSaveOrOpenBlob) {
            navigator.msSaveBlob(blob, fileName);
        } else {
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = fileName;
            link.click();
            window.URL.revokeObjectURL(link.href);
        }
    }
})();