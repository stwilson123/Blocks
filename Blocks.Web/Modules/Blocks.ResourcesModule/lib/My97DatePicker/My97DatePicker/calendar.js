if ($cfg.eCont) {
    $dp = {};
    for (var p in $pdp) {
        if (typeof $pdp[p] == "object") {
            $dp[p] = {};
            for (var pp in $pdp[p]) {
                $dp[p][pp] = $pdp[p][pp]
            }
        } else {
            $dp[p] = $pdp[p]
        }
    }
} else {
    $dp = $pdp
}
for (var p in $cfg) {
    $dp[p] = $cfg[p]
}
var $c;
if ($FF) {
    Event.prototype.__defineSetter__("returnValue",
        function(v) {
            if (!v) {
                this.preventDefault()
            }
            return v
        });
    Event.prototype.__defineGetter__("srcElement",
        function() {
            var node = this.target;
            while (node.nodeType != 1) {
                node = node.parentNode
            }
            return node
        })
}
function My97DP() {
    $c = this;
    this.QS = [];
    $d = document.createElement("div");
    $d.className = "WdateDiv";
    $d.innerHTML = '<div id=dpTitle><div class="NavImg NavImgll"><a></a></div><div class="NavImg NavImgl"><a></a></div><div style="float:left"><div class="menuSel MMenu"></div><input class=yminput></div><div style="float:left"><div class="menuSel YMenu"></div><input class=yminput></div><div class="NavImg NavImgrr"><a></a></div><div class="NavImg NavImgr"><a></a></div><div style="float:right"></div></div><div style="position:absolute;overflow:hidden"></div><div></div><div id=dpTime><div class="menuSel hhMenu"></div><div class="menuSel mmMenu"></div><div class="menuSel ssMenu"></div><table cellspacing=0 cellpadding=0 border=0><tr><td rowspan=2><span id=dpTimeStr></span>&nbsp;<input class=tB maxlength=2><input value=":" class=tm readonly><input class=tE maxlength=2><input value=":" class=tm readonly><input class=tE maxlength=2></td><td><button id=dpTimeUp></button></td></tr><tr><td><button id=dpTimeDown></button></td></tr></table></div><div id=dpQS></div><div id=dpControl><input class=dpButton id=dpClearInput type=button><input class=dpButton id=dpTodayInput type=button><input class=dpButton id=dpOkInput type=button></div>';
    attachTabEvent($d,
        function() {
            hideSel()
        });
    _initPoint();
    this.init();
    $dp.focusArr = [document, $d.MI, $d.yI, $d.HI, $d.mI, $d.sI, $d.clearI, $d.todayI, $d.okI];
    for (var i = 0; i < $dp.focusArr.length; i++) {
        var currCtrl = $dp.focusArr[i];
        currCtrl.nextCtrl = i == $dp.focusArr.length - 1 ? $dp.focusArr[1] : $dp.focusArr[i + 1];
        $dp.attachEvent(currCtrl, "onkeydown", _tab)
    }
    _initNavImg();
    _inputBindEvent("y,M,H,m,s");
    $d.upButton.onclick = function() {
        updownEvent(1)
    };
    $d.downButton.onclick = function() {
        updownEvent( - 1)
    };
    $d.qsDiv.onclick = function() {
        if ($d.qsDivSel.style.display != "block") {
            $c._fillQS();
            showB($d.qsDivSel)
        } else {
            hide($d.qsDivSel)
        }
    };
    document.body.appendChild($d);
    function _initPoint() {
        var as = gets("a");
        divs = gets("div"),
            ipts = gets("input"),
            btns = gets("button"),
            spans = gets("span");
        $d.navLeftImg = as[0];
        $d.leftImg = as[1];
        $d.rightImg = as[3];
        $d.navRightImg = as[2];
        $d.rMD = divs[9];
        $d.MI = ipts[0];
        $d.yI = ipts[1];
        $d.titleDiv = divs[0];
        $d.MD = divs[4];
        $d.yD = divs[6];
        $d.qsDivSel = divs[10];
        $d.dDiv = divs[11];
        $d.tDiv = divs[12];
        $d.HD = divs[13];
        $d.mD = divs[14];
        $d.sD = divs[15];
        $d.qsDiv = divs[16];
        $d.qsDiv.title = $lang.quickStr;
        $d.bDiv = divs[17];
        $d.HI = ipts[2];
        $d.mI = ipts[4];
        $d.sI = ipts[6];
        $d.clearI = ipts[7];
        $d.todayI = ipts[8];
        $d.okI = ipts[9];
        $d.upButton = btns[0];
        $d.downButton = btns[1];
        $d.timeSpan = spans[0];
        function gets(s) {
            return $d.getElementsByTagName(s)
        }
    }
    function _initNavImg() {
        $d.navLeftImg.onclick = function() {
            $ny = $ny <= 0 ? $ny - 1 : -1;
            if ($ny % 5 == 0) {
                $d.yI.focus();
                return
            }
            $d.yI.value = $dt.y - 1;
            $d.yI.onblur()
        };
        $d.leftImg.onclick = function() {
            $dt.attr("M", -1);
            $d.MI.onblur()
        };
        $d.rightImg.onclick = function() {
            $dt.attr("M", 1);
            $d.MI.onblur()
        };
        $d.navRightImg.onclick = function() {
            $ny = $ny >= 0 ? $ny + 1 : 1;
            if ($ny % 5 == 0) {
                $d.yI.focus();
                return
            }
            $d.yI.value = $dt.y + 1;
            $d.yI.onblur()
        }
    }
}
My97DP.prototype = {
    init: function() {
        $ny = 0;
        $dp.cal = this;
        if ($dp.readOnly && $dp.el.readOnly != null) {
            $dp.el.readOnly = true;
            $dp.el.blur()
        }
        this._dealFmt();
        $dt = this.newdate = new DPDate();
        $tdt = new DPDate();
        $sdt = this.date = new DPDate();
        $dp.valueEdited = 0;
        this.dateFmt = this.doExp($dp.dateFmt);
        this.autoPickDate = $dp.autoPickDate == null ? ($dp.has.st && $dp.has.st ? false: true) : $dp.autoPickDate;
        $dp.autoUpdateOnChanged = $dp.autoUpdateOnChanged == null ? ($dp.isShowOK && $dp.has.d ? false: true) : $dp.autoUpdateOnChanged;
        this.ddateRe = this._initRe("disabledDates");
        this.ddayRe = this._initRe("disabledDays");
        this.sdateRe = this._initRe("specialDates");
        this.sdayRe = this._initRe("specialDays");
        this.minDate = this.doCustomDate($dp.minDate, $dp.minDate != $dp.defMinDate ? $dp.realFmt: $dp.realFullFmt, $dp.defMinDate);
        this.maxDate = this.doCustomDate($dp.maxDate, $dp.maxDate != $dp.defMaxDate ? $dp.realFmt: $dp.realFullFmt, $dp.defMaxDate);
        this.minTime = this.doCustomDate($dp.minTime, $dp.realTimeFmt);
        this.maxTime = this.doCustomDate($dp.maxTime, $dp.realTimeFmt);
        if (this.minDate.compareWith(this.maxDate) > 0) {
            $dp.errMsg = $lang.err_1
        }
        if (this.loadDate()) {
            this._makeDateInRange();
            this.oldValue = $dp.el[$dp.elProp]
        } else {
            this.mark(false, 2)
        }
        _setAll($dt);
        $d.timeSpan.innerHTML = $lang.timeStr;
        $d.clearI.value = $lang.clearStr;
        $d.todayI.value = $lang.todayStr;
        $d.okI.value = $lang.okStr;
        $d.okI.disabled = !$c.checkValid($sdt);
        this.initShowAndHide();
        this.initBtn();
        if ($dp.errMsg) {
            alert($dp.errMsg)
        }
        this.draw();
        if ($dp.el.nodeType == 1 && $dp.el.My97Mark === undefined) {
            $dp.attachEvent($dp.el, "onkeydown", _tab);
            $dp.attachEvent($dp.el, "onblur",
                function() {
                    if ($dp && $dp.dd.style.display == "none") {
                        $c.close();
                        if (!$dp.valueEdited && $dp.cal.oldValue != $dp.el[$dp.elProp] && $dp.el.onchange) {
                            fireEvent($dp.el, "change")
                        }
                    }
                });
            $dp.el.My97Mark = false
        }
        $c.currFocus = $dp.el;
        hideSel()
    },
    _makeDateInRange: function() {
        var rv = this.checkRange();
        if (rv != 0) {
            var dt;
            if (rv > 0) {
                dt = this.maxDate
            } else {
                dt = this.minDate
            }
            if ($dp.has.sd) {
                $dt.y = dt.y;
                $dt.M = dt.M;
                $dt.d = dt.d
            }
            if ($dp.has.st) {
                $dt.H = dt.H;
                $dt.m = dt.m;
                $dt.s = dt.s
            }
        }
    },
    splitDate: function(str, fmt, y, M, d, H, m, s, b3x) {
        var dt;
        if (str && str.loadDate) {
            dt = str
        } else {
            dt = new DPDate();
            fmt = fmt || $dp.dateFmt;
            var i, splitStr, offset = 0,
                match, tokenRe = /yyyy|yyy|yy|y|MMMM|MMM|MM|M|dd|d|%ld|HH|H|mm|m|ss|s|DD|D|WW|W|w/g;
            var g = fmt.match(tokenRe);
            tokenRe.lastIndex = 0;
            if (b3x) {
                match = str.split(/\W+/)
            } else {
                var ii = 0,
                    reg = "^";
                while ((match = tokenRe.exec(fmt)) !== null) {
                    if (ii >= 0) {
                        splitStr = fmt.substring(ii, match.index);
                        if (splitStr && "-/\\".indexOf(splitStr) >= 0) {
                            splitStr = "[\\-/]"
                        }
                        reg += splitStr
                    }
                    ii = tokenRe.lastIndex;
                    switch (match[0]) {
                        case "yyyy":
                            reg += "(\\d{4})";
                            break;
                        case "yyy":
                            reg += "(\\d{3})";
                            break;
                        case "MMMM":
                        case "MMM":
                        case "DD":
                        case "D":
                            reg += "(\\D+)";
                            break;
                        default:
                            reg += "(\\d\\d?)";
                            break
                    }
                }
                reg += ".*$";
                match = new RegExp(reg).exec(str);
                offset = 1
            }
            if (match) {
                for (i = 0; i < g.length; i++) {
                    var v = match[i + offset];
                    if (v) {
                        switch (g[i]) {
                            case "MMMM":
                            case "MMM":
                                dt.M = getMonth(g[i], v);
                                break;
                            case "y":
                            case "yy":
                                v = pInt2(v, 0);
                                if (v < 50) {
                                    v += 2000
                                } else {
                                    v += 1900
                                }
                                dt.y = v;
                                break;
                            case "yyy":
                                dt.y = pInt2(v, 0) + $dp.yearOffset;
                                break;
                            default:
                                dt[g[i].slice( - 1)] = v;
                                break
                        }
                    }
                }
                dt.refresh()
            } else {
                if (str != "") {
                    dt.d = 32
                }
            }
        }
        dt.coverDate(y, M, d, H, m, s);
        return dt;
        function getMonth(fmt, v) {
            var arr = fmt == "MMMM" ? $lang.aLongMonStr: $lang.aMonStr;
            for (var i = 0; i < 12; i++) {
                if (arr[i].toLowerCase() == v.substr(0, arr[i].length).toLowerCase()) {
                    return i + 1
                }
            }
            return - 1
        }
    },
    _initRe: function(p) {
        var i, v = $dp[p],
            re = "";
        if (v && v.length > 0) {
            for (i = 0; i < v.length; i++) {
                re += this.doExp(v[i]);
                if (i != v.length - 1) {
                    re += "|"
                }
            }
            re = re ? new RegExp("(?:" + re + ")") : null
        } else {
            re = null
        }
        return re
    },
    update: function(v) {
        if (v === undefined) {
            v = this.getNewDateStr()
        }
        if ($dp.el[$dp.elProp] != v) {
            $dp.el[$dp.elProp] = v
        }
        this.setRealValue()
    },
    setRealValue: function(v) {
        var vel = $dp.$($dp.vel),
            v = rtn(v, this.getNewDateStr($dp.realFmt));
        if (vel) {
            vel.value = v
        }
        $dp.el.realValue = v
    },
    doExp: function(s) {
        var ps = "yMdHms",
            arr, tmpEval, re = /#?\{(.*?)\}/;
        s = s + "";
        for (var i = 0; i < ps.length; i++) {
            s = s.replace("%" + ps.charAt(i), this.getP(ps.charAt(i), null, $tdt))
        }
        if (s.substring(0, 3) == "#F{") {
            s = s.substring(3, s.length - 1);
            if (s.indexOf("return ") < 0) {
                s = "return " + s
            }
            s = $dp.win.eval('new Function("' + s + '");');
            s = s()
        }
        while ((arr = re.exec(s)) != null) {
            arr.lastIndex = arr.index + arr[1].length + arr[0].length - arr[1].length - 1;
            tmpEval = pInt(eval(arr[1]));
            if (tmpEval < 0) {
                tmpEval = "9700" + ( - tmpEval)
            }
            s = s.substring(0, arr.index) + tmpEval + s.substring(arr.lastIndex + 1)
        }
        return s
    },
    doCustomDate: function(s, fmt, defV) {
        var dt;
        s = this.doExp(s);
        if (!s || s == "") {
            s = defV
        }
        if (typeof s == "object") {
            dt = s
        } else {
            dt = this.splitDate(s, fmt, null, null, 1, 0, 0, 0, true);
            dt.y = ("" + dt.y).replace(/^9700/, "-");
            dt.M = ("" + dt.M).replace(/^9700/, "-");
            dt.d = ("" + dt.d).replace(/^9700/, "-");
            dt.H = ("" + dt.H).replace(/^9700/, "-");
            dt.m = ("" + dt.m).replace(/^9700/, "-");
            dt.s = ("" + dt.s).replace(/^9700/, "-");
            if (s.indexOf("%ld") >= 0) {
                s = s.replace(/%ld/g, "0")
            }
            dt.refresh()
        }
        return dt
    },
    loadDate: function() {
        var v = $dp.el[$dp.elProp],
            f = this.dateFmt,
            hs = $dp.has;
        if ($dp.alwaysUseStartDate || ($dp.startDate != "" && v == "")) {
            v = this.doExp($dp.startDate);
            f = $dp.realFmt
        }
        $dt.loadFromDate(this.splitDate(v, f));
        if (v != "") {
            var rv = 1;
            if (hs.sd && !this.isDate($dt)) {
                $dt.y = $tdt.y;
                $dt.M = $tdt.M;
                $dt.d = $tdt.d;
                rv = 0
            }
            if (hs.st && !this.isTime($dt)) {
                $dt.H = $tdt.H;
                $dt.m = $tdt.m;
                $dt.s = $tdt.s;
                rv = 0
            }
            return rv && this.checkValid($dt)
        }
        if (!hs.H) {
            $dt.H = 0
        }
        if (!hs.m) {
            $dt.m = 0
        }
        if (!hs.s) {
            $dt.s = 0
        }
        return 1
    },
    isDate: function(dt) {
        if (dt.y != null) {
            dt = doStr(dt.y, 4) + "-" + dt.M + "-" + dt.d
        }
        return dt.match(/^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[0-9])|([1-2][0-3]))\:([0-5]?[0-9])((\s)|(\:([0-5]?[0-9])))))?$/)
    },
    isTime: function(d) {
        if (d.H != null) {
            d = d.H + ":" + d.m + ":" + d.s
        }
        return d.match(/^([0-9]|([0-1][0-9])|([2][0-3])):([0-9]|([0-5][0-9])):([0-9]|([0-5][0-9]))$/)
    },
    checkRange: function(dt, p) {
        dt = dt || $dt;
        var v = dt.compareWith(this.minDate, p);
        if (v > 0) {
            v = dt.compareWith(this.maxDate, p);
            if (v < 0) {
                v = 0
            }
        }
        return v
    },
    checkTimeRange: function(dt, p) {
        dt = dt || $dt;
        if (dt.compareWith(this.minTime, p, "Hms") < 0) {
            return - 1
        } else {
            if (dt.compareWith(this.maxTime, p, "Hms") > 0) {
                return 1
            }
        }
        return 0
    },
    checkValid: function(dt, p, k) {
        p = p || $dp.has.minUnit;
        var v = this.checkRange(dt, p);
        if (v == 0) {
            v = 1;
            if (p == "d" && k == null) {
                k = Math.abs((new Date(dt.y, dt.M - 1, dt.d).getDay() - $dp.firstDayOfWeek + 7) % 7)
            }
            v = !this.testDisDay(k) && !this.testDisDate(dt, p);
            if ("Hms".indexOf(p) >= 0) {
                v = this.checkTimeRange(dt, p) == 0
            }
        } else {
            v = 0
        }
        return v
    },
    checkAndUpdate: function() {
        var el = $dp.el,
            c = this,
            v = $dp.el[$dp.elProp];
        if ($dp.errDealMode >= 0 && $dp.errDealMode <= 2 && v != null) {
            if (v != "") {
                c.date.loadFromDate(c.splitDate(v, $dp.dateFmt))
            }
            if (v == "" || (c.isDate(c.date) && c.isTime(c.date) && c.checkValid(c.date))) {
                if (v != "") {
                    c.newdate.loadFromDate(c.date);
                    c.update()
                } else {
                    c.setRealValue("")
                }
            } else {
                return false
            }
        }
        return true
    },
    close: function(e) {
        hideSel();
        if (this.checkAndUpdate()) {
            this.mark(true);
            $dp.hide()
        } else {
            if (e) {
                _cancelKey(e);
                this.mark(false, 2)
            } else {
                this.mark(false)
            }
            $dp.show()
        }
    },
    _fd: function() {
        var i, j, k, isShow, firstDate, s = new sb(),
            wkStr = $lang.aWeekStr,
            firstDay = $dp.firstDayOfWeek;
        var classStr = "",
            classOnStr = "",
            dt = new DPDate($dt.y, $dt.M, $dt.d, 2, 0, 0);
        var y = dt.y,
            M = dt.M;
        firstDate = 1 - new Date(y, M - 1, 1).getDay() + firstDay;
        if (firstDate > 1) {
            firstDate -= 7
        }
        s.a("<table class=WdayTable width=100% border=0 cellspacing=0 cellpadding=0>");
        s.a("<tr class=MTitle align=center>");
        if ($dp.isShowWeek) {
            s.a("<td>" + wkStr[0] + "</td>")
        }
        for (i = 0; i < 7; i++) {
            s.a("<td>" + wkStr[(firstDay + i) % 7 + 1] + "</td>")
        }
        s.a("</tr>");
        for (i = 1, j = firstDate; i < 7; i++) {
            s.a("<tr>");
            for (k = 0; k < 7; k++) {
                dt.loadDate(y, M, j++);
                dt.refresh();
                if (dt.M == M) {
                    isShow = true;
                    if (dt.compareWith($sdt, "d") == 0) {
                        classStr = "Wselday"
                    } else {
                        if (dt.compareWith($tdt, "d") == 0) {
                            classStr = "Wtoday"
                        } else {
                            classStr = ($dp.highLineWeekDay && (0 == (firstDay + k) % 7 || 6 == (firstDay + k) % 7) ? "Wwday": "Wday")
                        }
                    }
                    classOnStr = ($dp.highLineWeekDay && (0 == (firstDay + k) % 7 || 6 == (firstDay + k) % 7) ? "WwdayOn": "WdayOn")
                } else {
                    if ($dp.isShowOthers) {
                        isShow = true;
                        classStr = "WotherDay";
                        classOnStr = "WotherDayOn"
                    } else {
                        isShow = false
                    }
                }
                if ($dp.isShowWeek && k == 0 && (i < 4 || isShow)) {
                    s.a("<td class=Wweek>" + getWeek(dt, $dp.firstDayOfWeek == 0 ? 1 : 0) + "</td>")
                }
                s.a("<td ");
                if (isShow) {
                    if (this.checkValid(dt, "d", k)) {
                        if (this.testSpeDay(Math.abs((new Date(dt.y, dt.M - 1, dt.d).getDay() - $dp.firstDayOfWeek + 7) % 7)) || this.testSpeDate(dt)) {
                            classStr = "WspecialDay"
                        }
                        s.a('onclick="day_Click(' + dt.y + "," + dt.M + "," + dt.d + ');" ');
                        s.a("onmouseover=\"this.className='" + classOnStr + "'\" ");
                        s.a("onmouseout=\"this.className='" + classStr + "'\" ")
                    } else {
                        classStr = "WinvalidDay"
                    }
                    s.a("class=" + classStr);
                    s.a(">" + dt.d + "</td>")
                } else {
                    s.a("></td>")
                }
            }
            s.a("</tr>")
        }
        s.a("</table>");
        return s.j()
    },
    testDisDate: function(d, p) {
        var v = this.testDate(d, this.ddateRe, p);
        return (this.ddateRe && $dp.opposite) ? !v: v
    },
    testDisDay: function(d) {
        return this.testDay(d, this.ddayRe)
    },
    testSpeDate: function(d) {
        return this.testDate(d, this.sdateRe)
    },
    testSpeDay: function(d) {
        return this.testDay(d, this.sdayRe)
    },
    testDate: function(d, re, p) {
        var fmt = p == "d" ? $dp.realDateFmt: $dp.realFmt;
        if (p == "d" && $dp.has.d && $dp.opposite) {
            re = (re + "").replace(/^\/\(\?:(.*)\)\/.*/, "$1");
            var splitIndex = re.indexOf($dp.dateSplitStr);
            if (splitIndex >= 0) {
                re = re.substr(0, splitIndex)
            }
            re = new RegExp(re)
        }
        return re ? re.test(this.getDateStr(fmt, d)) : 0
    },
    testDay: function(k, re) {
        return re ? re.test(k) : 0
    },
    _f: function(p, max, c, r, e, isR) {
        var s = new sb(),
            fp = isR ? "r" + p: p;
        if (isR) {
            $dt.attr("M", 1)
        }
        bak = $dt[p];
        s.a("<table cellspacing=0 cellpadding=3 border=0");
        for (var i = 0; i < r; i++) {
            s.a('<tr nowrap="nowrap">');
            for (var j = 0; j < c; j++) {
                s.a("<td nowrap ");
                $dt[p] = eval(e);
                if ($dt[p] > max) {
                    s.a("class='menu'")
                } else {
                    if (this.checkValid($dt, p) || ($dp.opposite && "Hms".indexOf(p) == -1 && this.checkRange($dt, p) == 0)) {
                        s.a("class='menu' onmouseover=\"this.className='menuOn'\" onmouseout=\"this.className='menu'\" onmousedown=\"");
                        s.a("hide($d." + p + "D);$d." + fp + "I.value=" + $dt[p] + ";_blur.call($d." + fp + 'I);"')
                    } else {
                        s.a("class='invalidMenu'")
                    }
                }
                s.a(">");
                if ($dt[p] <= max) {
                    s.a(p == "M" ? $lang.aMonStr[$dt[p] - 1] : $dt[p])
                }
                s.a("</td>")
            }
            s.a("</tr>")
        }
        s.a("</table>");
        $dt[p] = bak;
        if (isR) {
            $dt.attr("M", -1)
        }
        return s.j()
    },
    _fMyPos: function(el, div) {
        if (el) {
            var left = el.offsetLeft;
            if ($IE) {
                left = el.getBoundingClientRect().left
            }
            div.style.left = left
        }
    },
    _fM: function(el) {
        this._fMyPos(el, $d.MD);
        $d.MD.innerHTML = this._f("M", 12, 2, 6, "i+j*6+1", el == $d.rMI)
    },
    _fy: function(el, minV, isR) {
        var s = new sb();
        isR = isR || el == $d.ryI;
        minV = Math.max(0, rtn(minV, $dt.y - 5));
        s.a(this._f("y", 9999, 2, 5, minV + "+i+j*5", isR));
        s.a("<table cellspacing=0 cellpadding=3 border=0 align=center><tr><td ");
        s.a(this.minDate.y < minV ? "class='menu' onmouseover=\"this.className='menuOn'\" onmouseout=\"this.className='menu'\" onmousedown='if(event.preventDefault)event.preventDefault();event.cancelBubble=true;$c._fy(0," + (minV - 10) + "," + isR + ")'": "class='invalidMenu'");
        s.a(">&#8592;</td><td class='menu' onmouseover=\"this.className='menuOn'\" onmouseout=\"this.className='menu'\" onmousedown=\"hide($d.yD);$d.yI.blur();\">&#215;</td><td ");
        s.a(this.maxDate.y >= minV + 10 ? "class='menu' onmouseover=\"this.className='menuOn'\" onmouseout=\"this.className='menu'\" onmousedown='if(event.preventDefault)event.preventDefault();event.cancelBubble=true;$c._fy(0," + (minV + 10) + "," + isR + ")'": "class='invalidMenu'");
        s.a(">&#8594;</td></tr></table>");
        this._fMyPos(el, $d.yD);
        $d.yD.innerHTML = s.j()
    },
    _fHMS: function(p, total) {
        var cfg = $dp.hmsMenuCfg[p],
            offset = cfg[0],
            col = cfg[1];
        $d[p + "D"].innerHTML = this._f(p, total - 1, col, Math.ceil(total / offset / col), "i*" + col + "*" + offset + "+j*" + offset)
    },
    _fH: function() {
        this._fHMS("H", 24)
    },
    _fm: function() {
        this._fHMS("m", 60)
    },
    _fs: function() {
        this._fHMS("s", 60)
    },
    _fillQS: function(bFlat, about) {
        this.initQS();
        var title = about ? [">a/<rekci", "PetaD 79y", 'M>knalb_=tegrat "eulb:roloc"=elyts "ten.79ym.w', 'ww//:ptth"=ferh a<'].join("").split("").reverse().join("") : $lang.quickStr;
        var qs = this.QS,
            qss = qs.style,
            s = new sb();
        s.a("<table class=WdayTable width=100% height=100% border=0 cellspacing=0 cellpadding=0>");
        s.a('<tr class=MTitle><td><div style="float:left">' + title + "</div>");
        if (!bFlat) {
            s.a('<div style="float:right;cursor:pointer" onclick="hide($d.qsDivSel);">X&nbsp;</div>')
        }
        s.a("</td></tr>");
        for (var i = 0; i < qs.length; i++) {
            if (qs[i]) {
                s.a("<tr><td style='text-align:left' nowrap='nowrap' class='menu' onmouseover=\"this.className='menuOn'\" onmouseout=\"this.className='menu'\" onclick=\"");
                s.a("day_Click(" + qs[i].y + ", " + qs[i].M + ", " + qs[i].d + "," + qs[i].H + "," + qs[i].m + "," + qs[i].s + ');">');
                s.a("&nbsp;" + this.getDateStr(null, qs[i]));
                s.a("</td></tr>")
            } else {
                s.a("<tr><td class='menu'>&nbsp;</td></tr>")
            }
        }
        s.a("</table>");
        $d.qsDivSel.innerHTML = s.j()
    },
    _dealFmt: function() {
        _setHas(/w/);
        _setHas(/WW|W/);
        _setHas(/DD|D/);
        _setHas(/yyyy|yyy|yy|y/);
        _setHas(/MMMM|MMM|MM|M/);
        _setHas(/dd|d/);
        _setHas(/HH|H/);
        _setHas(/mm|m/);
        _setHas(/ss|s/);
        $dp.has.sd = ($dp.has.y || $dp.has.M || $dp.has.d) ? true: false;
        $dp.has.st = ($dp.has.H || $dp.has.m || $dp.has.s) ? true: false;
        var splitMatch = $dp.realFullFmt.match(/%Date(.*)%Time/);
        $dp.dateSplitStr = splitMatch ? splitMatch[1] : " ";
        $dp.realFullFmt = $dp.realFullFmt.replace(/%Date/, $dp.realDateFmt).replace(/%Time/, $dp.realTimeFmt);
        if ($dp.has.sd) {
            if ($dp.has.st) {
                $dp.realFmt = $dp.realFullFmt
            } else {
                $dp.realFmt = $dp.realDateFmt
            }
        } else {
            $dp.realFmt = $dp.realTimeFmt
        }
        function _setHas(re) {
            var p = (re + "").slice(1, 2);
            $dp.has[p] = re.exec($dp.dateFmt) ? ($dp.has.minUnit = p, true) : false
        }
    },
    initShowAndHide: function() {
        var hasYorM = 0;
        $dp.has.y ? (hasYorM = 1, show($d.yI, $d.navLeftImg, $d.navRightImg)) : hide($d.yI, $d.navLeftImg, $d.navRightImg);
        $dp.has.M ? (hasYorM = 1, show($d.MI, $d.leftImg, $d.rightImg)) : hide($d.MI, $d.leftImg, $d.rightImg);
        hasYorM ? show($d.titleDiv) : hide($d.titleDiv);
        if ($dp.has.st) {
            show($d.tDiv);
            disHMS($d.HI, $dp.has.H);
            disHMS($d.mI, $dp.has.m);
            disHMS($d.sI, $dp.has.s)
        } else {
            hide($d.tDiv)
        }
        shorH($d.clearI, $dp.isShowClear);
        shorH($d.todayI, $dp.isShowToday);
        shorH($d.okI, $dp.isShowOK);
        shorH($d.qsDiv, !$dp.doubleCalendar && $dp.has.d && $dp.qsEnabled);
        if ($dp.eCont || !($dp.isShowClear || $dp.isShowToday || $dp.isShowOK)) {
            hide($d.bDiv)
        } else {
            show($d.bDiv)
        }
    },
    mark: function(b, mode) {
        var el = $dp.el;
        var cls = $FF ? "class": "className";
        if ($dp.errDealMode == -1) {
            return
        } else {
            if (b) {
                _unmark(el)
            } else {
                if (mode == null) {
                    mode = $dp.errDealMode
                }
                switch (mode) {
                    case 0:
                        if (confirm($lang.errAlertMsg)) {
                            el[$dp.elProp] = this.oldValue || "";
                            _unmark(el)
                        } else {
                            _mark(el)
                        }
                        break;
                    case 1:
                        el[$dp.elProp] = this.oldValue || "";
                        _unmark(el);
                        break;
                    case 2:
                        _mark(el);
                        break
                }
            }
        }
        function _unmark(el) {
            var cn = el.className;
            if (cn) {
                var s = cn.replace(/WdateFmtErr/g, "");
                if (cn != s) {
                    try {
                        el.setAttribute(cls, s)
                    } catch(e) {
                        alert("WdatePicker:el is null")
                    }
                }
            }
        }
        function _mark(el) {
            try {
                el.setAttribute(cls, el.className + " WdateFmtErr")
            } catch(e) {
                alert("WdatePicker:el is null")
            }
        }
    },
    getP: function(p, f, dt) {
        dt = dt || $sdt;
        var i, r = [p + p, p],
            ri,
            v = dt[p];
        var getV = function(ri) {
            return doStr(v, ri.length)
        };
        switch (p) {
            case "w":
                v = getDay(dt);
                break;
            case "D":
                var tempD = getDay(dt) + 1;
                getV = function(ri) {
                    return ri.length == 2 ? $lang.aLongWeekStr[tempD] : $lang.aWeekStr[tempD]
                };
                break;
            case "W":
                v = getWeek(dt);
                break;
            case "y":
                r = ["yyyy", "yyy", "yy", "y"];
                f = f || r[0];
                getV = function(ri) {
                    return doStr((ri.length < 4) ? (ri.length < 3 ? dt.y % 100 : (dt.y + 2000 - $dp.yearOffset) % 1000) : v, ri.length)
                };
                break;
            case "M":
                r = ["MMMM", "MMM", "MM", "M"];
                getV = function(ri) {
                    return (ri.length == 4) ? $lang.aLongMonStr[v - 1] : (ri.length == 3) ? $lang.aMonStr[v - 1] : doStr(v, ri.length)
                };
                break
        }
        f = f || p + p;
        if ("yMdHms".indexOf(p) > -1 && p != "y" && !$dp.has[p]) {
            if ("Hms".indexOf(p) > -1) {
                v = 0
            } else {
                v = 1
            }
        }
        var values = [];
        for (i = 0; i < r.length; i++) {
            ri = r[i];
            if (f.indexOf(ri) >= 0) {
                values[i] = getV(ri);
                f = f.replace(new RegExp(ri, "g"), "{" + i + "}")
            }
        }
        for (i = 0; i < values.length; i++) {
            f = f.replace(new RegExp("\\{" + i + "\\}", "g"), values[i])
        }
        return f
    },
    getDateStr: function(f, dt) {
        dt = dt || this.splitDate($dp.el[$dp.elProp], this.dateFmt) || $sdt;
        f = f || this.dateFmt;
        if (f.indexOf("%ld") >= 0) {
            var tmpDT = new DPDate();
            tmpDT.loadFromDate(dt);
            tmpDT.d = 0;
            tmpDT.M = pInt(tmpDT.M) + 1;
            tmpDT.refresh();
            f = f.replace(/%ld/g, tmpDT.d)
        }
        var s = "ydHmswW";
        for (var i = 0; i < s.length; i++) {
            var p = s.charAt(i);
            f = this.getP(p, f, dt)
        }
        if (f.indexOf("D") >= 0) {
            f = f.replace(/DD/g, "%dd").replace(/D/g, "%d");
            f = this.getP("M", f, dt);
            f = f.replace(/\%dd/g, this.getP("D", "DD", dt)).replace(/\%d/g, this.getP("D", "D", dt))
        } else {
            f = this.getP("M", f, dt)
        }
        return f
    },
    getNewP: function(p, f) {
        return this.getP(p, f, $dt)
    },
    getNewDateStr: function(f) {
        return this.getDateStr(f, this.newdate)
    },
    draw: function() {
        $c._dealFmt();
        $d.rMD.innerHTML = "";
        if ($dp.doubleCalendar) {
            $c.autoPickDate = true;
            $dp.isShowOthers = false;
            $d.className = "WdateDiv WdateDiv2";
            var s = new sb();
            s.a("<table class=WdayTable2 width=100% cellspacing=0 cellpadding=0 border=1><tr><td valign=top>");
            s.a(this._fd());
            s.a("</td><td valign=top>");
            $dt.attr("M", 1);
            s.a(this._fd());
            $d.rMI = $d.MI.cloneNode(true);
            $d.ryI = $d.yI.cloneNode(true);
            $d.rMD.appendChild($d.rMI);
            $d.rMD.appendChild($d.ryI);
            $d.rMI.value = $lang.aMonStr[$dt.M - 1];
            $d.rMI.realValue = $dt.M;
            $d.ryI.value = $dt.y;
            _inputBindEvent("rM,ry");
            $d.rMI.className = $d.ryI.className = "yminput";
            $dt.attr("M", -1);
            s.a("</td></tr></table>");
            $d.dDiv.innerHTML = s.j()
        } else {
            $d.className = "WdateDiv";
            $d.dDiv.innerHTML = this._fd()
        }
        if (!$dp.has.d || $dp.autoShowQS) {
            this._fillQS(true);
            showB($d.qsDivSel)
        } else {
            hide($d.qsDivSel)
        }
        this.autoSize()
    },
    autoSize: function() {
        var ifs = parent.document.getElementsByTagName("iframe");
        for (var i = 0; i < ifs.length; i++) {
            var bh = $d.style.height;
            $d.style.height = "";
            var h = $d.offsetHeight;
            if (ifs[i].contentWindow == window && h) {
                ifs[i].style.width = $d.offsetWidth + "px";
                var th = $d.tDiv.offsetHeight;
                if (th && $d.bDiv.style.display == "none" && $d.tDiv.style.display != "none" && document.body.scrollHeight - h >= th) {
                    h += th;
                    $d.style.height = h
                } else {
                    $d.style.height = bh
                }
                ifs[i].style.height = Math.max(h, $d.offsetHeight) + "px"
            }
        }
        $d.qsDivSel.style.width = $d.dDiv.offsetWidth + "px";
        $d.qsDivSel.style.height = $d.dDiv.offsetHeight + "px"
    },
    pickDate: function() {
        $dt.d = Math.min(new Date($dt.y, $dt.M, 0).getDate(), $dt.d);
        $sdt.loadFromDate($dt);
        $dp.valueEdited = 0;
        this.update();
        if (!$dp.eCont) {
            if (this.checkValid($dt)) {
                elFocus();
                hide($dp.dd)
            }
        }
        if ($dp.onpicked) {
            callFunc("onpicked")
        }
    },
    initBtn: function() {
        $d.clearI.onclick = function() {
            if (!callFunc("onclearing")) {
                $dp.valueEdited = 0;
                $c.update("");
                elFocus();
                hide($dp.dd);
                if ($dp.oncleared) {
                    callFunc("oncleared")
                }
            }
        };
        $d.okI.onclick = function() {
            day_Click()
        };
        if (this.checkValid($tdt)) {
            $d.todayI.disabled = false;
            $d.todayI.onclick = function() {
                $dt.loadFromDate($tdt);
                day_Click()
            }
        } else {
            $d.todayI.disabled = true
        }
    },
    initQS: function() {
        var i, j, d, rv, arr = [],
            total = 5,
            l = $dp.quickSel.length,
            u = $dp.has.minUnit;
        if (l > total) {
            l = total
        } else {
            if (u == "m" || u == "s") {
                arr = [ - 60, -30, 0, 30, 60, -15, 15, -45, 45]
            } else {
                for (i = 0; i < total + 9; i++) {
                    arr[i] = $dt[u] - 2 + i
                }
            }
        }
        for (i = j = 0; i < l; i++) {
            d = this.doCustomDate($dp.quickSel[i]);
            if (this.checkValid(d)) {
                this.QS[j++] = d
            }
        }
        var s = "yMdHms",
            tmpArr = [1, 1, 1, 0, 0, 0];
        for (i = 0; i <= s.indexOf(u); i++) {
            tmpArr[i] = $dt[s.charAt(i)]
        }
        for (i = 0; j < total; i++) {
            if (i < arr.length) {
                d = new DPDate(tmpArr[0], tmpArr[1], tmpArr[2], tmpArr[3], tmpArr[4], tmpArr[5]);
                d[u] = arr[i];
                d.refresh();
                if (this.checkValid(d)) {
                    this.QS[j++] = d
                }
            } else {
                this.QS[j++] = null
            }
        }
    }
};
function elFocus() {
    var el = $dp.el;
    try {
        if (el.style.display != "none" && el.type != "hidden" && (el.nodeName.toLowerCase() == "input" || el.nodeName.toLowerCase() == "textarea")) {
            el.My97Mark = true;
            el.focus()
        }
    } catch(e) {}
    setTimeout(function() {
            el.My97Mark = false
        },
        197)
}
function sb() {
    this.s = new Array();
    this.i = 0;
    this.a = function(t) {
        this.s[this.i++] = t
    };
    this.j = function() {
        return this.s.join("")
    }
}
function getWeek(dt, offset) {
    offset = offset || 0;
    var d = new Date(dt.y, dt.M - 1, dt.d + offset),
        weekNum;
    if ($dp.weekMethod == "ISO8601") {
        d.setDate(d.getDate() - (d.getDay() + 6) % 7 + 3);
        var ms = d.valueOf();
        d.setMonth(0);
        d.setDate(4);
        weekNum = Math.round((ms - d.valueOf()) / (7 * 86400000)) + 1
    } else {
        var d2 = new Date(dt.y, 0, 1);
        d = Math.round((d.valueOf() - d2.valueOf()) / 86400000);
        weekNum = Math.ceil((d + (d2.getDay() + 1)) / 7)
    }
    return ($dt.M == 1 && weekNum > 52) ? 1 : weekNum
}
function getDay(dt) {
    var d = new Date(dt.y, dt.M - 1, dt.d);
    return d.getDay()
}
function show() {
    setDisp(arguments, "")
}
function showB() {
    setDisp(arguments, "block")
}
function hide() {
    setDisp(arguments, "none")
}
function setDisp(args, v) {
    for (i = 0; i < args.length; i++) {
        args[i].style.display = v
    }
}
function shorH(el, bExp) {
    bExp ? show(el) : hide(el)
}
function disHMS(el, bExp) {
    if (bExp) {
        el.disabled = false
    } else {
        el.disabled = true;
        el.value = "00"
    }
}
function c(p, pv) {
    var rv, v = pv;
    if (p == "M") {
        v = makeInRange(pv, 1, 12)
    } else {
        if (p == "H") {
            v = makeInRange(pv, 0, 23)
        } else {
            if ("ms".indexOf(p) >= 0) {
                v = makeInRange(pv, 0, 59)
            }
        }
    }
    if ("Hms".indexOf(p) >= 0) {
        rv = $c.checkTimeRange($dt, p);
        if (rv < 0) {
            _setTime($c.minTime);
            v = $c.minTime[p]
        } else {
            if (rv > 0) {
                _setTime($c.maxTime);
                v = $c.maxTime[p]
            }
        }
    }
    $dt[p] = v;
    if ($d[p + "I"]) {
        if (p == "M") {
            $d.MI.realValue = v;
            $d.MI.value = $lang.aMonStr[v - 1]
        } else {
            $d[p + "I"].value = v
        }
    }
    if ($sdt[p] != v && !callFunc(p + "changing")) {
        rv = $c.checkRange();
        if (rv == 0) {
            sv(p, v)
        } else {
            if (rv < 0) {
                _setAll($c.minDate)
            } else {
                if (rv > 0) {
                    _setAll($c.maxDate)
                }
            }
        }
        $d.okI.disabled = !$c.checkValid($sdt);
        if ("yMd".indexOf(p) >= 0) {
            $c.draw()
        }
        callFunc(p + "changed")
    }
}
function _setTime(o) {
    sv("H", o.H);
    sv("m", o.m);
    sv("s", o.s)
}
function _setAll(o) {
    sv("y", o.y);
    sv("M", o.M);
    sv("d", o.d);
    _setTime(o)
}
function day_Click(y, M, d, H, m, s) {
    var bak = new DPDate($dt.y, $dt.M, $dt.d, $dt.H, $dt.m, $dt.s);
    $dt.loadDate(y, M, d, H, m, s);
    if (!callFunc("onpicking")) {
        var isCurrDay = bak.y == y && bak.M == M && bak.d == d;
        if (!isCurrDay && arguments.length != 0) {
            c("y", y);
            c("M", M);
            c("d", d);
            if ($dp.has.st) {
                updownEvent(0)
            }
            $c.currFocus = $dp.el;
            dealAutoUpdate()
        }
        if ($c.autoPickDate || isCurrDay || arguments.length == 0) {
            $c.pickDate()
        }
    } else {
        $dt = bak
    }
}
function dealAutoUpdate() {
    if ($dp.autoUpdateOnChanged) {
        try {
            $c.update();
            $dp.el.focus()
        } catch(e) {}
    }
}
function callFunc(eventName) {
    var rv;
    if ($dp[eventName]) {
        rv = $dp[eventName].call($dp.el, $dp)
    }
    return rv
}
function sv(p, v) {
    if (v == null) {
        v = $dt[p]
    }
    $sdt[p] = $dt[p] = v;
    if ("yHms".indexOf(p) >= 0) {
        $d[p + "I"].value = v
    }
    if (p == "M") {
        $d.MI.realValue = v;
        $d.MI.value = $lang.aMonStr[v - 1]
    }
}
function makeInRange(v, min, max) {
    if (v < min) {
        v = min
    } else {
        if (v > max) {
            v = max
        }
    }
    return v
}
function attachTabEvent(o, func) {
    $dp.attachEvent(o, "onkeydown",
        function(e) {
            e = e || event,
                k = (e.which == undefined) ? e.keyCode: e.which;
            if (k == 9) {
                func()
            }
        })
}
function doStr(s, len) {
    s = s + "";
    while (s.length < len) {
        s = "0" + s
    }
    return s
}
function hideSel() {
    hide($d.yD, $d.MD, $d.HD, $d.mD, $d.sD)
}
function updownEvent(offset) {
    var cf = $c.currFocus,
        hmsCfg = $dp.hmsMenuCfg;
    if (cf != $d.HI && cf != $d.mI && cf != $d.sI) {
        cf = $d.HI
    }
    var i, p = cf == $d.HI ? "H": (cf == $d.mI ? "m": "s"),
        interval = hmsCfg[p][0],
        v = $dt[p] + offset * interval;
    for (i = 0; i <= 60; i += interval) {
        if (v <= i) {
            v = (i - v) < interval / 2 ? i: Math.max(0, i - interval);
            if (v == 60) {
                v--
            }
            break
        }
    }
    $dt[p] = v;
    c(p, $dt[p]);
    dealAutoUpdate();
    function doEvent(p, v) {
        var interval = $dp.hmsMenuCfg[p][0];
        for (var i = 0; i <= 60; i += interval) {
            if (v <= i) {
                v = (i - v) < interval / 2 ? i: Math.max(0, i - interval);
                if (v == 60) {
                    v = 59
                }
                break
            }
        }
    }
}
function DPDate(y, M, d, H, m, s) {
    this.loadDate(y, M, d, H, m, s)
}
DPDate.prototype = {
    loadDate: function(y, M, d, H, m, s) {
        var dt = new Date();
        this.y = pInt3(y, this.y, dt.getFullYear());
        this.M = pInt3(M, this.M, dt.getMonth() + 1);
        this.d = $dp.has.d ? pInt3(d, this.d, dt.getDate()) : 1;
        this.H = pInt3(H, this.H, dt.getHours());
        this.m = pInt3(m, this.m, dt.getMinutes());
        this.s = pInt3(s, this.s, dt.getSeconds())
    },
    loadFromDate: function(o) {
        if (o) {
            this.loadDate(o.y, o.M, o.d, o.H, o.m, o.s)
        }
    },
    coverDate: function(y, M, d, H, m, s) {
        var dt = new Date();
        this.y = pInt3(this.y, y, dt.getFullYear());
        this.M = pInt3(this.M, M, dt.getMonth() + 1);
        this.d = $dp.has.d ? pInt3(this.d, d, dt.getDate()) : 1;
        this.H = pInt3(this.H, H, dt.getHours());
        this.m = pInt3(this.m, m, dt.getMinutes());
        this.s = pInt3(this.s, s, dt.getSeconds())
    },
    compareWith: function(dt, p, s) {
        s = s || "yMdHms";
        var v, tp;
        p = s.indexOf(p);
        p = p >= 0 ? p: 5;
        for (var i = 0; i <= p; i++) {
            tp = s.charAt(i);
            v = this[tp] - dt[tp];
            if (v > 0) {
                return 1
            } else {
                if (v < 0) {
                    return - 1
                }
            }
        }
        return 0
    },
    refresh: function() {
        if (this.d == "ld") {
            this.d = 0;
            this.M = pInt(this.M) + 1
        }
        var dt = new Date(this.y, this.M - 1, this.d, this.H, this.m, this.s);
        if (this.y < 1900) {
            dt.setFullYear(this.y)
        }
        this.y = dt.getFullYear();
        this.M = dt.getMonth() + 1;
        this.d = dt.getDate();
        this.H = dt.getHours();
        this.m = dt.getMinutes();
        this.s = dt.getSeconds();
        return ! isNaN(this.y)
    },
    attr: function(p, v) {
        if ("yMdHms".indexOf(p) >= 0) {
            var pback = this.d;
            if (p == "M") {
                this.d = 1
            }
            this[p] += v;
            this.refresh();
            this.d = pback
        }
    }
};
function pInt(n) {
    return parseInt(n, 10)
}
function pInt2(v1, v2) {
    return rtn(pInt(v1), v2)
}
function pInt3(v1, v2, v3) {
    return pInt2(v1, rtn(v2, v3))
}
function rtn(v1, v2) {
    return v1 == null || isNaN(v1) ? v2: v1
}
function fireEvent(o, evtName) {
    if ($IE) {
        o.fireEvent("on" + evtName)
    } else {
        var evt = document.createEvent("HTMLEvents");
        evt.initEvent(evtName, true, true);
        o.dispatchEvent(evt)
    }
}
function _foundInput(el) {
    var p, i, arr = "y,M,H,m,s,ry,rM".split(",");
    for (i = 0; i < arr.length; i++) {
        p = arr[i];
        if ($d[p + "I"] == el) {
            return p.slice(p.length - 1, p.length)
        }
    }
    return 0
}
function _focus(e) {
    var p = _foundInput(this),
        pDiv = $d[p + "D"];
    if (!p) {
        return
    }
    $c.currFocus = this;
    $c.lastFocusTime = new Date();
    if (p == "y") {
        this.className = "yminputfocus"
    } else {
        if (p == "M") {
            this.className = "yminputfocus";
            this.value = this["realValue"]
        }
    }
    try {
        this.select()
    } catch(e) {}
    $c["_f" + p](this);
    showB(pDiv);
    if ("Hms".indexOf(p) >= 0) {
        pDiv.style.marginLeft = Math.min(this.offsetLeft, $d.sI.offsetLeft + 60 - pDiv.offsetWidth);
        pDiv.style.marginTop = this.offsetTop - pDiv.offsetHeight - 2
    }
}
function _blur(showDiv) {
    if (new Date() - $c.lastFocusTime < 97) {
        return
    }
    var p = _foundInput(this),
        isR,
        mStr,
        v = this.value,
        oldv = $dt[p];
    if (p == 0) {
        return
    }
    $dt[p] = Number(v) >= 0 ? Number(v) : $dt[p];
    if (p == "y") {
        isR = this == $d.ryI;
        if (isR && $dt.M == 12) {
            $dt.y -= 1
        }
    } else {
        if (p == "M") {
            isR = this == $d.rMI;
            if (isR) {
                mStr = $lang.aMonStr[$dt[p] - 1];
                if (oldv == 12) {
                    $dt.y += 1
                }
                $dt.attr("M", -1)
            }
            if ($sdt.M == $dt.M) {
                this.value = mStr || $lang.aMonStr[$dt[p] - 1]
            }
            if (($sdt.y != $dt.y)) {
                c("y", $dt.y)
            }
        }
    }
    eval('c("' + p + '",' + $dt[p] + ")");
    if (showDiv !== true) {
        if (p == "y" || p == "M") {
            this.className = "yminput"
        }
        hide($d[p + "D"])
    }
    dealAutoUpdate()
}
function _cancelKey(e) {
    if (e.preventDefault) {
        e.preventDefault();
        e.stopPropagation()
    } else {
        e.cancelBubble = true;
        e.returnValue = false
    }
    if ($OPERA) {
        e.keyCode = 0
    }
}
function _inputBindEvent(str) {
    var _arr = str.split(",");
    for (var i = 0; i < _arr.length; i++) {
        var _p = _arr[i] + "I";
        $d[_p].onfocus = _focus;
        $d[_p].onblur = _blur
    }
}
function _tab(e) {
    var curr = e.srcElement || e.target,
        k = e.which || e.keyCode,
        isShow = $dp.eCont ? true: $dp.dd.style.display != "none";
    $dp.valueEdited = 1;
    if (k >= 96 && k <= 105) {
        k -= 48
    }
    if ($dp.enableKeyboard && isShow) {
        if (!curr.nextCtrl) {
            curr.nextCtrl = $dp.focusArr[1];
            $c.currFocus = $dp.el
        }
        if (curr == $dp.el) {
            $c.currFocus = $dp.el
        }
        if (k == 27) {
            if (curr == $dp.el) {
                $c.close();
                return
            } else {
                $dp.el.focus()
            }
        }
        if (k >= 37 && k <= 40) {
            var p;
            if ($c.currFocus == $dp.el || $c.currFocus == $d.okI) {
                if ($dp.has.d) {
                    p = "d";
                    if (k == 38) {
                        $dt[p] -= 7
                    } else {
                        if (k == 39) {
                            $dt[p] += 1
                        } else {
                            if (k == 37) {
                                $dt[p] -= 1
                            } else {
                                $dt[p] += 7
                            }
                        }
                    }
                    $dt.refresh();
                    c("y", $dt.y);
                    c("M", $dt.M);
                    c("d", $dt[p]);
                    _cancelKey(e);
                    return
                } else {
                    p = $dp.has.minUnit;
                    $d[p + "I"].focus()
                }
            }
            p = p || _foundInput($c.currFocus);
            if (p) {
                if (k == 38 || k == 39) {
                    $dt[p] += 1
                } else {
                    $dt[p] -= 1
                }
                $dt.refresh();
                $c.currFocus.value = $dt[p];
                _blur.call($c.currFocus, true);
                $c.currFocus.select()
            }
        } else {
            if (k == 9) {
                var next = curr.nextCtrl;
                for (var i = 0; i < $dp.focusArr.length; i++) {
                    if (next.disabled == true || next.offsetHeight == 0) {
                        next = next.nextCtrl
                    } else {
                        break
                    }
                }
                if ($c.currFocus != next) {
                    $c.currFocus = next;
                    next.focus()
                }
            } else {
                if (k == 13) {
                    _blur.call($c.currFocus);
                    if ($c.currFocus.type == "button") {
                        $c.currFocus.click()
                    } else {
                        if ($dp.cal.oldValue == $dp.el[$dp.elProp]) {
                            $c.pickDate()
                        } else {
                            $c.close()
                        }
                    }
                    $c.currFocus = $dp.el
                }
            }
        }
    } else {
        if (k == 9 && curr == $dp.el) {
            $c.close()
        }
    }
    if ($dp.enableInputMask && !$OPERA && !$dp.readOnly && $c.currFocus == $dp.el && (k >= 48 && k <= 57)) {
        var el = $dp.el,
            v = el.value,
            pos = getPosition(el),
            ss = {
                str: "",
                arr: []
            },
            i = 0,
            mm,
            vStart = 0,
            vEnd = 0,
            n = 0,
            offset,
            tokenRe = /yyyy|yyy|yy|y|MM|M|dd|d|%ld|HH|H|mm|m|ss|s|WW|W|w/g,
            g = $dp.dateFmt.match(tokenRe),
            t1,
            t2,
            t3,
            t4,
            t5,
            vlen,
            offset = 0;
        if (v != "") {
            n = v.match(/[0-9]/g);
            n = n == null ? 0 : n.length;
            for (i = 0; i < g.length; i++) {
                n -= Math.max(g[i].length, 2)
            }
            n = n >= 0 ? 1 : 0;
            if (n == 1 && pos >= v.length) {
                pos = v.length - 1
            }
        }
        v = v.substring(0, pos) + String.fromCharCode(k) + v.substring(pos + n);
        pos++;
        for (i = 0; i < v.length; i++) {
            var vi = v.charAt(i);
            if (/[0-9]/.test(vi)) {
                ss.str += vi
            } else {
                ss.arr[i] = 1
            }
        }
        v = "";
        tokenRe.lastIndex = 0;
        while ((mm = tokenRe.exec($dp.dateFmt)) !== null) {
            vEnd = mm.index - (mm[0] == "%ld" ? 1 : 0);
            if (vStart >= 0) {
                v += $dp.dateFmt.substring(vStart, vEnd);
                if (pos >= vStart + offset && pos <= vEnd + offset) {
                    pos += vEnd - vStart
                }
            }
            vStart = tokenRe.lastIndex;
            vlen = vStart - vEnd;
            t1 = ss.str.substring(0, vlen);
            t2 = mm[0].charAt(0);
            t3 = pInt(t1.charAt(0));
            if (ss.str.length > 1) {
                t4 = ss.str.charAt(1);
                t5 = t3 * 10 + pInt(t4)
            } else {
                t4 = "";
                t5 = t3
            }
            if (ss.arr[vEnd + 1] || t2 == "M" && t5 > 12 || t2 == "d" && t5 > 31 || t2 == "H" && t5 > 23 || "ms".indexOf(t2) >= 0 && t5 > 59) {
                if (mm[0].length == 2) {
                    t1 = "0" + t3
                } else {
                    t1 = t3
                }
                pos++
            } else {
                if (vlen == 1) {
                    t1 = t5;
                    vlen++;
                    offset++
                }
            }
            v += t1;
            ss.str = ss.str.substring(vlen);
            if (ss.str == "") {
                break
            }
        }
        el.value = v;
        setPosition(el, pos);
        _cancelKey(e)
    }
    if (isShow && $c.currFocus != $dp.el && !((k >= 48 && k <= 57) || k == 8 || k == 46)) {
        _cancelKey(e)
    }
    function getPosition(ctrl) {
        var CaretPos = 0;
        if ($dp.win.document.selection) {
            var sel = $dp.win.document.selection.createRange(),
                sellength = sel.text.length;
            sel.moveStart("character", -ctrl.value.length);
            CaretPos = sel.text.length - sellength
        } else {
            if (ctrl.selectionStart || ctrl.selectionStart == "0") {
                CaretPos = ctrl.selectionStart
            }
        }
        return CaretPos
    }
    function setPosition(ctrl, pos) {
        if (ctrl.setSelectionRange) {
            ctrl.focus();
            ctrl.setSelectionRange(pos, pos)
        } else {
            if (ctrl.createTextRange) {
                var range = ctrl.createTextRange();
                range.collapse(true);
                range.moveEnd("character", pos);
                range.moveStart("character", pos);
                range.select()
            }
        }
    }
}
document.ready = 1;