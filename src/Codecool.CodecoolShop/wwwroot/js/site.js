﻿updateLocalStorage();

dropdownMenu('.category-select', 'Filter Categories');
dropdownMenu('.supplier-select', 'Filter Suppliers');

function updateLocaleStorageOnCheckAll(datastring) {
    var dropdownLabel = document.querySelector(`.dropdown-option[data-string=${datastring}]`);
    var dropString = dropdownLabel.dataset.string;
    var dropdownList = Object.keys(localStorage)
        .filter(key => key.includes(dropString));

    if (dropdownLabel.innerHTML !== "Check All") {
        for (var drop of dropdownList) {
            localStorage[drop] = "true";
        }
    }
    else {
        for (var drop of dropdownList) {
            localStorage[drop] = "false";
        }
    }
}

function updateLocalStorage() {
    var boxes = document.querySelectorAll(".checkBox-category, .checkBox-supplier");
    for (var i = 0; i < boxes.length; i++) {
        var box = boxes[i];
        if (box.hasAttribute("value")) {
            setupBox(box);
        }
    }
    function setupBox(box) {
        var storageId = box.getAttribute("value");
        var oldVal = localStorage.getItem(storageId);
        box.checked = oldVal === "true" ? true : false;
        box.addEventListener("change", function () {
            localStorage.setItem(storageId, this.checked);
        });
    }
};

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

            this.$checkAll.on('click', function(e) {
                e.preventDefault();
                _this.onCheckAll();
                updateLocaleStorageOnCheckAll(this.dataset.string);
                _this.refresh();

            });

            this.$inputs.on('change', function (e) {
                _this.onCheckBox();
                _this.refresh();
            });
        };

        CheckboxDropdown.prototype.onCheckBox = function () {
            this.updateStatus();
        };

        CheckboxDropdown.prototype.refresh = function () {
            var categoryList = Object.keys(localStorage).filter(key => localStorage[key] === "true" && key.includes("category-"));
            var categoryString = categoryList.join(",");
            categoryString = categoryString.replaceAll("category-", "");
            var supplierList = Object.keys(localStorage).filter(key => localStorage[key] === "true" && key.includes("supplier-"));
            var supplierString = supplierList.join(",");
            supplierString = supplierString.replaceAll("supplier-", "");

            if (categoryString.length > 0 && supplierString.length > 0) {
                window.location.href = "https://localhost:44368/" +
                    "Product?categories=" +
                    categoryString +
                    "&suppliers=" +
                    supplierString;
            }
            if (categoryString.length > 0 && supplierString.length === 0) {
                window.location.href = "https://localhost:44368/" + "Product?categories=" + categoryString;
            }
            if (categoryString.length === 0 && supplierString.length > 0) {
                window.location.href = "https://localhost:44368/" + "Product?suppliers=" + supplierString;
            }
            if (categoryString.length === 0 && supplierString.length === 0) {
                window.location.href = "https://localhost:44368/";
            }
        }
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