window.agencyInterop = {
    copyToClipboard: async function (text) {
        if (!navigator.clipboard) {
            var textArea = document.createElement("textarea");
            textArea.value = text;
            document.body.appendChild(textArea);
            textArea.focus();
            textArea.select();
            try {
                document.execCommand('copy');
            } catch (err) {
                console.error('Fallback copy failed', err);
            }
            document.body.removeChild(textArea);
            return true;
        }
        try {
            await navigator.clipboard.writeText(text);
            return true;
        } catch (err) {
            console.error('Clipboard copy error', err);
            return false;
        }
    },

    printHtml: function (htmlContent) {
        var printIframe = document.getElementById('contract-print-iframe');
        if (!printIframe) {
            printIframe = document.createElement('iframe');
            printIframe.id = 'contract-print-iframe';
            printIframe.style.position = 'fixed';
            printIframe.style.right = '0';
            printIframe.style.bottom = '0';
            printIframe.style.width = '0';
            printIframe.style.height = '0';
            printIframe.style.border = '0';
            document.body.appendChild(printIframe);
        }

        var doc = printIframe.contentWindow.document;
        doc.open();
        doc.write(htmlContent);
        doc.close();

        setTimeout(function () {
            printIframe.contentWindow.focus();
            printIframe.contentWindow.print();
        }, 300);
    },

    setupLockInput: function (dotNetHelper) {
        var focusInput = function () {
            try {
                window.focus();
                if (document.body) document.body.focus();
                var input = document.getElementById('agency-master-pin-input');
                if (input) {
                    input.focus();
                }
            } catch (e) { }
        };

        focusInput();
        [50, 150, 300, 600, 1000].forEach(function (t) {
            setTimeout(focusInput, t);
        });

        if (window._lockDocClickHandler) {
            document.removeEventListener('click', window._lockDocClickHandler, true);
        }
        window._lockDocClickHandler = function (e) {
            if (!e.target || !e.target.closest('button')) {
                focusInput();
            }
        };
        document.addEventListener('click', window._lockDocClickHandler, true);

        if (window._agencyLockHandler) {
            window.removeEventListener('keydown', window._agencyLockHandler, true);
        }

        window._agencyLockHandler = function (e) {
            var target = e.target;
            var isPinInput = target && target.id === 'agency-master-pin-input';

            var digit = null;
            if (e.key >= '0' && e.key <= '9') {
                digit = e.key;
            } else if (e.key >= '٠' && e.key <= '٩') {
                digit = String(e.key.charCodeAt(0) - 1632);
            } else if (e.key >= '۰' && e.key <= '۹') {
                digit = String(e.key.charCodeAt(0) - 1776);
            } else if (e.code && e.code.startsWith('Numpad') && e.code.length === 7) {
                digit = e.code.replace('Numpad', '');
            }

            if (digit !== null) {
                if (!isPinInput) {
                    e.preventDefault();
                    dotNetHelper.invokeMethodAsync('HandleKeyInput', digit);
                }
            } else if (e.key === 'Backspace') {
                if (!isPinInput) {
                    e.preventDefault();
                    dotNetHelper.invokeMethodAsync('HandleBackspace');
                }
            } else if (e.key === 'Escape' || e.key === 'Delete') {
                e.preventDefault();
                dotNetHelper.invokeMethodAsync('HandleClear');
            } else if (e.key === 'Enter') {
                e.preventDefault();
                dotNetHelper.invokeMethodAsync('HandleSubmit');
            }
        };

        window.addEventListener('keydown', window._agencyLockHandler, true);
    },

    removeLockInput: function () {
        if (window._agencyLockHandler) {
            window.removeEventListener('keydown', window._agencyLockHandler, true);
            window._agencyLockHandler = null;
        }
        if (window._lockDocClickHandler) {
            document.removeEventListener('click', window._lockDocClickHandler, true);
            window._lockDocClickHandler = null;
        }
    }
};
