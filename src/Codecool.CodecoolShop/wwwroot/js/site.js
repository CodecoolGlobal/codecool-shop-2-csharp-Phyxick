dropdownMenu('.category-select', 'Filter Categories');
dropdownMenu('.supplier-select', 'Filter Suppliers');

function dropdownMenu(dropdown, dropdownText) {

    (function ($) {
        var CheckboxDropdown = function (el) {
            var _this = this;
            this.isOpen = false;
            this.areAllChecked = false;
            this.$el = $(el);
            this.$label = this.$el.find(dropdown);
            this.$checkAll = this.$el.find('[data-toggle="check-all"]').first();
            this.$inputs = this.$el.find('[type="checkbox"]');

            this.onCheckBox();

            this.$label.on('click', function (e) {
                e.preventDefault();
                _this.toggleOpen();
            });

            this.$checkAll.on('click', function (e) {
                e.preventDefault();
                _this.onCheckAll();
            });

            this.$inputs.on('change', function (e) {
                _this.onCheckBox();
            });
        };

        CheckboxDropdown.prototype.onCheckBox = function () {
            this.updateStatus();
        };

        CheckboxDropdown.prototype.updateStatus = function () {
            var checked = this.$el.find(':checked');

            this.areAllChecked = false;
            this.$checkAll.html('Check All');

            if (checked.length <= 0) {
                this.$label.html(dropdownText);
            }
            else if (checked.length === 1) {
                this.$label.html(checked.parent('label').text());
            }
            else if (checked.length === this.$inputs.length) {
                this.$label.html('All Selected');
                this.areAllChecked = true;
                this.$checkAll.html('Uncheck All');
            }
            else {
                this.$label.html(checked.length + ' Selected');
            }
        };

        CheckboxDropdown.prototype.onCheckAll = function (checkAll) {
            if (!this.areAllChecked || checkAll) {
                this.areAllChecked = true;
                this.$checkAll.html('Uncheck All');
                this.$inputs.prop('checked', true);
            }
            else {
                this.areAllChecked = false;
                this.$checkAll.html('Check All');
                this.$inputs.prop('checked', false);
            }

            this.updateStatus();
        };

        CheckboxDropdown.prototype.toggleOpen = function (forceOpen) {
            var _this = this;

            if (!this.isOpen || forceOpen) {
                this.isOpen = true;
                this.$el.addClass('on');
                $(document).on('click', function (e) {
                    if (!$(e.target).closest('[data-control]').length) {
                        _this.toggleOpen();
                    }
                });
            }
            else {
                this.isOpen = false;
                this.$el.removeClass('on');
                $(document).off('click');
            }
        };

        var checkboxesDropdowns = document.querySelectorAll('[data-control="checkbox-dropdown"]');
        for (var i = 0, length = checkboxesDropdowns.length; i < length; i++) {
            new CheckboxDropdown(checkboxesDropdowns[i]);
        }
    })(jQuery);
};

let geolocation = document.querySelector("#geolocation");

function getCountry() {

    fetch('https://api.geoapify.com/v1/ipinfo?apiKey=01bd1e62308b42d987a328181041412e', {
            method: 'GET'
        })
        .then(function (response) { return response.json(); })
        .then(function (json) {
            geolocation.innerHTML = "We deliver to: " + json.country.name;
        });
}
getCountry();


function SetBilling(checked) {
    if (checked) {
        document.getElementById('deliveryaddres').style.display = "none";
    }
    else {
        document.getElementById('deliveryaddres').style.display = "block";
    }
}