(function ($, undefined) {
    var emptyStateValue = 'all';
    $.widget('qs.statusbar', {
        widgetPrefix: 'statusbar',
        options: {
            selectedValue: null,
            data: [],
            change: function () { },
            allText: '全部'
        },
        setDefaults: function (settings) {
            $.extend(this.options, settings || {});
            return this;
        },
        _create: function () {
            var self = this,
                options = self.options
            data = options.data || [];

            var statusBar = this.element.addClass('toolbar');
            var totalRecordCount = 0;

            function makeBarItem(item) {
                var li = $("<li></li>"),
                    anchor = $("<a></a>")
                        .html(item.Text)
                        .attr('href', '#' + item.Value)
                        .append('<font color="red">(' + item.RecordCount + ')</font>')
                        .click(function () {
                            var href = $(this).attr('href'),
                                hrefHash = href.split('#');

                            if (hrefHash && hrefHash[1]) {
                                if (self.select(hrefHash[1]) === false) {
                                    return false;
                                }
                            }
                            this.blur();
                            return false;
                        })
                        .appendTo(li);
                li.appendTo(statusBar);
            }

            $.each(data, function (i, item) {
                totalRecordCount += item.RecordCount;
                makeBarItem(item);
            });
            makeBarItem({
                Text: options.allText,
                Value: emptyStateValue,
                RecordCount: totalRecordCount
            });

            if (options.selectedValue && (typeof options.selectedValue === 'string' && options.selectedValue !== '')) {
                self.status(options.selectedValue);
            } else {
                self.status(emptyStateValue);
            }
        },
        status: function (value) {
            if (typeof value === "undefined")
                return this.selectedValue;
            this.selectedValue = value;
            this.element.find('[href=#' + value + ']').closest('li').addClass('current');
            return this;
        },
        select: function (value) {
            this.status(value);
            if (this._trigger('change', null, value) === false) {
                return false;
            }
            return this;
        }
    });
})(jQuery);