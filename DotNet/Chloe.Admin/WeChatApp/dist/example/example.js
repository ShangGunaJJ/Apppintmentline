
window.APPURL = "http://youxu.yunyuange.cc";
window.APPURL="http://localhost:7116";
$(function () {
    var pageManager = {
        $container: $('#container'),
        _pageStack: [],
        _PlaceId: "",
        _configs: [],
        _pageAppend: function () { },
        _defaultPage: null,
        _pageIndex: 1,
        setDefault: function (defaultPage) {
            this._defaultPage = this._find('name', defaultPage);
            return this;
        },
        setPageAppend: function (pageAppend) {
            this._pageAppend = pageAppend;
            return this;
        },
        init: function () {
            var self = this;

            $(window).on('hashchange', function () {
                var state = history.state || {};
                var url = location.hash.indexOf('#') === 0 ? location.hash : '#';
                var page = self._find('url', url) || self._defaultPage;
                if (state._pageIndex <= self._pageIndex || self._findInStack(url)) {
                    self._back(page);
                } else {
                    self._go(page);
                }
            });

            if (history.state && history.state._pageIndex) {
                this._pageIndex = history.state._pageIndex;
            }

            this._pageIndex--;

            var url = location.hash.indexOf('#') === 0 ? location.hash : '#';
            var page = self._find('url', url) || self._defaultPage;
            this._go(page);
            return this;
        },
        push: function (config) {
            this._configs.push(config);
            return this;
        },
        go: function (to) {
            var config = this._find('name', to);
            if (!config) {
                return;
            }
            location.hash = config.url;
        },
        _go: function (config) {
            this._pageIndex++;

            history.replaceState && history.replaceState({ _pageIndex: this._pageIndex }, '', location.href);

            var html = $(config.template).html();
            var $html = $(html).addClass('slideIn').addClass(config.name);
            $html.on('animationend webkitAnimationEnd', function () {
                $html.removeClass('slideIn').addClass('js_show');
            });
            this.$container.append($html);
            this._pageAppend.call(this, $html);
            this._pageStack.push({
                config: config,
                dom: $html
            });

            if (!config.isBind) {
                this._bind(config);
            }

            return this;
        },
        back: function () {
            history.back();
        },
        _back: function (config) {
            this._pageIndex--;

            var stack = this._pageStack.pop();
            if (!stack) {
                return;
            }

            var url = location.hash.indexOf('#') === 0 ? location.hash : '#';
            var found = this._findInStack(url);
            if (!found) {
                var html = $(config.template).html();
                var $html = $(html).addClass('js_show').addClass(config.name);
                $html.insertBefore(stack.dom);

                if (!config.isBind) {
                    this._bind(config);
                }

                this._pageStack.push({
                    config: config,
                    dom: $html
                });
            }

            stack.dom.addClass('slideOut').on('animationend webkitAnimationEnd', function () {
                stack.dom.remove();
            });

            return this;
        },
        _findInStack: function (url) {
            var found = null;
            for (var i = 0, len = this._pageStack.length; i < len; i++) {
                var stack = this._pageStack[i];
                if (stack.config.url === url) {
                    found = stack;
                    break;
                }
            }
            return found;
        },
        _find: function (key, value) {
            var page = null;
            for (var i = 0, len = this._configs.length; i < len; i++) {
                if (this._configs[i][key] === value) {
                    page = this._configs[i];
                    break;
                }
            }
            return page;
        },
        _bind: function (page) {
            var events = page.events || {};
            for (var t in events) {
                for (var type in events[t]) {
                    this.$container.on(type, t, events[t][type]);
                }
            }
            page.isBind = true;
        }
    };

    function fastClick() {
        var supportTouch = function () {
            try {
                document.createEvent("TouchEvent");
                return true;
            } catch (e) {
                return false;
            }
        }();
        var _old$On = $.fn.on;

        $.fn.on = function () {
            if (/click/.test(arguments[0]) && typeof arguments[1] == 'function' && supportTouch) { // 只扩展支持touch的当前元素的click事件
                var touchStartY, callback = arguments[1];
                _old$On.apply(this, ['touchstart', function (e) {
                    touchStartY = e.changedTouches[0].clientY;
                }]);
                _old$On.apply(this, ['touchend', function (e) {
                    if (Math.abs(e.changedTouches[0].clientY - touchStartY) > 10) return;

                    e.preventDefault();
                    callback.apply(this, [e]);
                }]);
            } else {
                _old$On.apply(this, arguments);
            }
            return this;
        };
    }
    function preload() {
        $(window).on("load", function () {
            var imgList = [
                "./images/layers/content.png",
                "./images/layers/navigation.png",
                "./images/layers/popout.png",
                "./images/layers/transparent.gif"
            ];
            for (var i = 0, len = imgList.length; i < len; ++i) {
                new Image().src = imgList[i];
            }
        });
    }
    function androidInputBugFix() {
        if (/Android/gi.test(navigator.userAgent)) {
            window.addEventListener('resize', function () {
                if (document.activeElement.tagName == 'INPUT' || document.activeElement.tagName == 'TEXTAREA') {
                    window.setTimeout(function () {
                        document.activeElement.scrollIntoViewIfNeeded();
                    }, 0);
                }
            })
        }
    }
    function setJSAPI() {
        var option = {
            title: 'WeUI, 为微信 Web 服务量身设计',
            desc: 'WeUI, 为微信 Web 服务量身设计',
            link: "https://weui.io",
            imgUrl: 'https://mmbiz.qpic.cn/mmemoticon/ajNVdqHZLLA16apETUPXh9Q5GLpSic7lGuiaic0jqMt4UY8P4KHSBpEWgM7uMlbxxnVR7596b3NPjUfwg7cFbfCtA/0'
        };

        $.getJSON('https://weui.io/api/sign?url=' + encodeURIComponent(location.href.split('#')[0]), function (res) {
            wx.config({
                beta: true,
                debug: false,
                appId: res.appid,
                timestamp: res.timestamp,
                nonceStr: res.nonceStr,
                signature: res.signature,
                jsApiList: [
                    'onMenuShareTimeline',
                    'onMenuShareAppMessage',
                    'onMenuShareQQ',
                    'onMenuShareWeibo',
                    'onMenuShareQZone',
                    // 'setNavigationBarColor',
                    'setBounceBackground'
                ]
            });
            wx.ready(function () {
                /*
                 wx.invoke('setNavigationBarColor', {
                 color: '#F8F8F8'
                 });
                 */
                wx.invoke('setBounceBackground', {
                    'backgroundColor': '#F8F8F8',
                    'footerBounceColor': '#F8F8F8'
                });
                wx.onMenuShareTimeline(option);
                wx.onMenuShareQQ(option);
                wx.onMenuShareAppMessage({
                    title: 'WeUI',
                    desc: '为微信 Web 服务量身设计',
                    link: location.href,
                    imgUrl: 'https://mmbiz.qpic.cn/mmemoticon/ajNVdqHZLLA16apETUPXh9Q5GLpSic7lGuiaic0jqMt4UY8P4KHSBpEWgM7uMlbxxnVR7596b3NPjUfwg7cFbfCtA/0'
                });
            });
        });
    }
    function setPageManager() {
        var pages = {}, tpls = $('script[type="text/html"]');
        var winH = $(window).height();

        for (var i = 0, len = tpls.length; i < len; ++i) {
            var tpl = tpls[i], name = tpl.id.replace(/tpl_/, '');
            pages[name] = {
                name: name,
                url: '#' + name,
                template: '#' + tpl.id
            };
        }
        var UserInfo = JSON.parse(localStorage.getItem("UserInfo"));
        if (UserInfo && UserInfo.Id) {
            pages.Default.url = '#';   
            pageManager.setDefault('Default');  
        } else {
            pages.Reg.url = '#';
            pageManager.setDefault('Reg');
        }
        for (var page in pages) {
            pageManager.push(pages[page]);
        }
        pageManager
            .setPageAppend(function ($html) {
                var $foot = $html.find('.page__ft');
                if ($foot.length < 1) return;

                if ($foot.position().top + $foot.height() < winH) {
                    $foot.addClass('j_bottom');
                } else {
                    $foot.removeClass('j_bottom');
                }
            });
        pageManager.init();
    }
    function GetUrlArgument(name) {
        var url = location.href.replace(/([^\?|&]*)=/g, function (rg) { return (rg || "").toLowerCase() });
        var val = new RegExp(name.toLowerCase() + "=([^&]*)").exec(url);
        return val && val[1] || "";
    }
    function ShowTool(msg) {
        var $tooltips = $('.js_tooltips');
        if (msg != '') {
            $tooltips.text(msg);
            $tooltips.css('display', 'block');
            setTimeout(function () {
                $tooltips.css('display', 'none');
            }, 2000);
        }
    }
    function init() {
        preload();
        fastClick();
        androidInputBugFix();
        //setJSAPI();
        setPageManager();
        LoadSetting();
        window.pageManager = pageManager;
        window.ShowTool = ShowTool; 
  
        window.Reg = function () {
            location.hash = '';
        };
    }
    function LoadSetting() {
        $.get(APPURL+"/WeChat/GetSet", function (x, status, xhr) {
            localStorage.setItem("Config", x);
            x = JSON.parse(x);
            console.log(x); 
            var CancelAppNum = 0;
            for (var i = 0; i < x.length; i++) {
                if (x[i].Id == "1ef03475df694efd954d78cc0de2a477" || x[i].KeyName == "每天同一业务预约次数")
                    localStorage.setItem("ToDayCount", x[i].Value);
                else if (x[i].Id == "8bdf7cba605a4adf8074d70c37930adb" || x[i].KeyName == "每月同一业务预约次数")
                    localStorage.setItem("monthCount", x[i].Value);
                else if (x[i].Id == "ee019c775a0c4a4ab1e7d4ff7ce121c4" || x[i].KeyName == "提前多少分钟可预约")
                    localStorage.setItem("InAdvCount", x[i].Value);
                else if (x[i].Id == "0c8d62b3d2f6425b878dfdad70d0fa76" || x[i].KeyName == "最长预约天数")
                    localStorage.setItem("AppLongDay", x[i].Value);
                else if (x[i].Id == "1a2d7cba822b4e53b6a7060342196eb2" || x[i].KeyName == "提前多少小时取消预约时间")
                    localStorage.setItem("CancelApp", x[i].Value);
                else if (x[i].Id == "f8d0276b709c490b972cb6fa47a5bd12" || x[i].KeyName == "开始时间") {
                    localStorage.setItem("StartAppTime", x[i].Value); 
                }
                else if (x[i].Id == "7aa00c799a24438abc8997e68e5fea7b" || x[i].KeyName == "结束时间") {
                    localStorage.setItem("EndAppTime", x[i].Value); 
                }
                else if (x[i].Id == "b47f57860388458395cec837e627a95e" || x[i].KeyName == "每月可取消预约次数") {
                    localStorage.setItem("CancelAppNum", x[i].Value);CancelAppNum=x[i].Value;
                }
            }
        });
    }


    init();
});