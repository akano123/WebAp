/*Nguyen Hung Manh*/
"use strict";
window.MyLib = window.MyLib || {};
MyLib.Ui = MyLib.Ui || {};

MyLib.Ui = (function (jqueryLib) {

    //---------MultiCheckBoxes---------
    function MultiCheckBoxes(options) {
        var self = this;
        if (!(self instanceof MultiCheckBoxes)) {
            return new MultiCheckBoxes(options);
        }

        //parsing parameters
        options = options || {};
        if (typeof options.checkboxAllSelector == 'undefined' || typeof options.checkboxListSelector == 'undefined') {
            throw {
                message: 'checkboxAllSelector or checkboxListSelector undefined',
                description: 'Parameters are required'
            }
        }

        //private fields
        var $checkboxAll = $(options.checkboxAllSelector),
            $checkboxList = $(options.checkboxListSelector);

        //public properties
        Object.defineProperty(self, "$CheckboxAll", {
            get: function () { return $checkboxAll; }
        });

        Object.defineProperty(self, "$CheckboxList", {
            get: function () { return $checkboxList; }
        });
    }

    MultiCheckBoxes.prototype = {
        init: function () {
            var self = this;

            //checkbox all
            self.$CheckboxAll.on('change', function () {
                var result = this.checked;
                var $chkItems = self.$CheckboxList.find('[type="checkbox"]');
                for (var i = 0; i < $chkItems.length; i++) {
                    $chkItems[i].checked = result;
                }
            });

            //checkboxed list
            self.$CheckboxList.on('change', '[type="checkbox"]', function () {
                var $chkItems = self.$CheckboxList.find('[type="checkbox"]'),
                    $selectedItems = self.$CheckboxList.find('[type="checkbox"]:checked');

                if ($chkItems.length === $selectedItems.length) {
                    self.$CheckboxAll.prop('checked', true);
                } else {
                    self.$CheckboxAll.prop('checked', false);
                }
            });
            return self;
        },
        getSelectedItemValues: function () {
            var self = this;
            var $selectedItems = self.$CheckboxList.find('[type="checkbox"]:checked');

            return $selectedItems.map(function (index, element) {
                return $(element).val();
            }).get();
        }
    }

    //---------Other Modules---------


    //---------Return Modules----------
    return {
        MultiCheckBoxes: MultiCheckBoxes
    }
})($);
