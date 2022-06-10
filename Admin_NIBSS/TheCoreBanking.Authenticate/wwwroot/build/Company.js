"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ko = require("knockout");
var $ = require("jquery");
var TheCoreBanking;
(function (TheCoreBanking) {
    var CompanyModel = (function () {
        function CompanyModel(company) {
            this.id = ko.observable(company.id);
            this.coyId = ko.observable(company.coyId);
            this.coyName = ko.observable(company.coyName);
            this.address = ko.observable(company.address);
            this.telephone = ko.observable(company.telephone);
            this.fax = ko.observable(company.fax);
            this.email = ko.observable(company.email);
            this.dateOfIncorporation = ko.observable(company.dateOfIncorporation);
            this.manager = ko.observable(company.manager);
            this.natureOfBusiness = ko.observable(company.natureOfBusiness);
            this.nameOfScheme = ko.observable(company.nameOfScheme);
            this.functionsRegistered = ko.observable(company.functionsRegistered);
            this.authorisedShareCapital = ko.observable(company.authorisedShareCapital);
            this.nameOfRegistrar = ko.observable(company.nameOfRegistrar);
            this.nameOfTrustees = ko.observable(company.nameOfTrustees);
            this.formerManagersTrustees = ko.observable(company.formerManagersTrustees);
            this.dateOfRenewalOfRegistration = ko.observable(company.dateOfRenewalOfRegistration);
            this.dateOfCommencement = ko.observable(company.dateOfCommencement);
            this.initialFloatation = ko.observable(company.initialFloatation);
            this.initialSubscription = ko.observable(company.initialSubscription);
            this.coyRegisteredBy = ko.observable(company.coyRegisteredBy);
            this.trusteesAddress = ko.observable(company.trusteesAddress);
            this.investmentObjective = ko.observable(company.investmentObjective);
            this.companyClass = ko.observable(company.companyClass);
            this.companyType = ko.observable(company.companyType);
            this.accountingStandard = ko.observable(company.accountingStandard);
            this.mgtType = ko.observable(company.mgtType);
            this.webbsite = ko.observable(company.webbsite);
            this.coyClass = ko.observable(company.coyClass);
            this.accountStand = ko.observable(company.accountStand);
            this.managementType = ko.observable(company.managementType);
            this.eoyprofitAndLossGl = ko.observable(company.eoyprofitAndLossGl);
        }
        CompanyModel.prototype.Insert = function (callback) {
            var data = '{"coyId":"' + this.coyId +
                '","coyName":"' + this.coyName +
                '","address":"' + this.address +
                '","telephone":"' + this.telephone +
                '","fax":"' + this.fax +
                '","email":"' + this.email +
                '","dateOfIncorporation":"' + this.dateOfIncorporation +
                '","manager":"' + this.manager +
                '","natureOfBusiness":"' + this.natureOfBusiness +
                '","nameOfScheme":"' + this.nameOfScheme +
                '","functionsRegistered":"' + this.functionsRegistered +
                '","authorisedShareCapital":"' + this.authorisedShareCapital +
                '","nameOfRegistrar":"' + this.nameOfRegistrar +
                '","nameOfTrustees":"' + this.nameOfTrustees +
                '","formerManagersTrustees":"' + this.formerManagersTrustees +
                '","dateOfRenewalOfRegistration":"' + this.dateOfRenewalOfRegistration +
                '","dateOfCommencement":"' + this.dateOfCommencement +
                '","initialFloatation":"' + this.initialFloatation +
                '","initialSubscription":"' + this.initialSubscription +
                '","coyRegisteredBy":"' + this.coyRegisteredBy +
                '","trusteesAddress":"' + this.trusteesAddress +
                '","investmentObjective":"' + this.investmentObjective +
                '","companyClass":"' + this.companyClass +
                '","companyType":"' + this.companyType +
                '","accountingStandard":"' + this.accountingStandard +
                '","mgtType":"' + this.mgtType +
                '","webbsite":"' + this.webbsite +
                '","coyClass":"' + this.coyClass +
                '","accountStand":"' + this.accountStand +
                '","managementType":"' + this.managementType +
                '","eoyprofitAndLossGl":"' + this.eoyprofitAndLossGl + '"}';
            alert('here');
            $.ajax({
                type: 'POST',
                url: '/CompanySetup/Add_Company/',
                data: data,
                dataType: 'json',
                success: callback,
                error: function () { alert('Error'); }
            });
            return 0;
        };
        CompanyModel.prototype.Update = function (callback) {
            var data = '{"coyId":"' + this.coyId +
                '","coyName":"' + this.coyName +
                '","address":"' + this.address +
                '","telephone":"' + this.telephone +
                '","fax":"' + this.fax +
                '","email":"' + this.email +
                '","dateOfIncorporation":"' + this.dateOfIncorporation +
                '","manager":"' + this.manager +
                '","natureOfBusiness":"' + this.natureOfBusiness +
                '","nameOfScheme":"' + this.nameOfScheme +
                '","functionsRegistered":"' + this.functionsRegistered +
                '","authorisedShareCapital":"' + this.authorisedShareCapital +
                '","nameOfRegistrar":"' + this.nameOfRegistrar +
                '","nameOfTrustees":"' + this.nameOfTrustees +
                '","formerManagersTrustees":"' + this.formerManagersTrustees +
                '","dateOfRenewalOfRegistration":"' + this.dateOfRenewalOfRegistration +
                '","dateOfCommencement":"' + this.dateOfCommencement +
                '","initialFloatation":"' + this.initialFloatation +
                '","initialSubscription":"' + this.initialSubscription +
                '","coyRegisteredBy":"' + this.coyRegisteredBy +
                '","trusteesAddress":"' + this.trusteesAddress +
                '","investmentObjective":"' + this.investmentObjective +
                '","companyClass":"' + this.companyClass +
                '","companyType":"' + this.companyType +
                '","accountingStandard":"' + this.accountingStandard +
                '","mgtType":"' + this.mgtType +
                '","webbsite":"' + this.webbsite +
                '","coyClass":"' + this.coyClass +
                '","accountStand":"' + this.accountStand +
                '","managementType":"' + this.managementType +
                '","eoyprofitAndLossGl":"' + this.eoyprofitAndLossGl + '"}';
            $.ajax({
                type: 'PUT',
                url: '/api/customers',
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: callback,
                error: function (xhr, err) {
                    alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                    alert("responseText: " + xhr.responseText);
                }
            });
            return 0;
        };
        CompanyModel.prototype.Delete = function (callback) {
            var data = '{"coyId":"' + this.coyId +
                '","coyName":"' + this.coyName +
                '","address":"' + this.address +
                '","telephone":"' + this.telephone +
                '","fax":"' + this.fax +
                '","email":"' + this.email +
                '","dateOfIncorporation":"' + this.dateOfIncorporation +
                '","manager":"' + this.manager +
                '","natureOfBusiness":"' + this.natureOfBusiness +
                '","nameOfScheme":"' + this.nameOfScheme +
                '","functionsRegistered":"' + this.functionsRegistered +
                '","authorisedShareCapital":"' + this.authorisedShareCapital +
                '","nameOfRegistrar":"' + this.nameOfRegistrar +
                '","nameOfTrustees":"' + this.nameOfTrustees +
                '","formerManagersTrustees":"' + this.formerManagersTrustees +
                '","dateOfRenewalOfRegistration":"' + this.dateOfRenewalOfRegistration +
                '","dateOfCommencement":"' + this.dateOfCommencement +
                '","initialFloatation":"' + this.initialFloatation +
                '","initialSubscription":"' + this.initialSubscription +
                '","coyRegisteredBy":"' + this.coyRegisteredBy +
                '","trusteesAddress":"' + this.trusteesAddress +
                '","investmentObjective":"' + this.investmentObjective +
                '","companyClass":"' + this.companyClass +
                '","companyType":"' + this.companyType +
                '","accountingStandard":"' + this.accountingStandard +
                '","mgtType":"' + this.mgtType +
                '","webbsite":"' + this.webbsite +
                '","coyClass":"' + this.coyClass +
                '","accountStand":"' + this.accountStand +
                '","managementType":"' + this.managementType +
                '","eoyprofitAndLossGl":"' + this.eoyprofitAndLossGl + '"}';
            $.ajax({
                type: 'DELETE',
                url: '/api/customers',
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: callback,
                error: function (xhr, err) {
                    alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                    alert("responseText: " + xhr.responseText);
                }
            });
            return 0;
        };
        CompanyModel.prototype.SelectAll = function (callback) {
            $.getJSON("api/customers", callback);
        };
        return CompanyModel;
    }());
    ko.applyBindings(CompanyModel);
})(TheCoreBanking || (TheCoreBanking = {}));
//# sourceMappingURL=Company.js.map