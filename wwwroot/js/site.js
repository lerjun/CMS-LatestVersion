const { black } = require("color-name");

var reloadLoading = {
  message: '<div class="loadingio-spinner-eclipse-xtl7ibbvv9p " style="margin:auto !important;left:0"><div class= "ldio-f9web4uhsr" style="margin:auto !important;left:0"><div style="margin:auto !important;left:0"></div></div></div>',
  blockMsgClass: "loading",
  baseZ: 2000,
  css: {
    backgroundColor: "transparent",
    border: "0",
  },
};

function notifyMsg(title, msg, color, icon) {
  iziToast.show({
    title: title,
    message: msg,
    theme: "light",
    color: color,
    icon: icon,
    transitionIn: "bounceInDown",
    transitionOut: "flipOutX",
    position: "topCenter",
  });
}

async function ShowprivilegeList() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/MembershipPrivilege/GetPrivilegeList", data: {}, type: "GET", datatype: "json" })
      .done(function (data) {
        privilegetable.clear().draw();
        // 
        for (var i = 0; i < data.length; i++) {
          var isVIP = "";
          var noExpiry = "";
          if (data[i].isVIP == 1) {
            isVIP = "YES";
          } else {
            isVIP = "NO";
          }
          if (data[i].noExpiry == 1) {
            noExpiry = "YES";
          } else {
            noExpiry = "NO";
          }
          var tdbuttons = ' <div class="data-img">' + "<img src=" + data[i].imgUrl + " alt=" + data[i].title + ' width="100%" />' + "</div>" + "<div>" + "<p>" + data[i].privilegeID + "</p>" + "<p>" + data[i].title + "</p>" + '<div class="actions"><a class="tbl-edit" data-id="' + data[i].id + '"  data-descrip="' + data[i].description + '" data-btypeid="' + data[i].businessTypeID + '" data-imgfile="' + data[i].imgUrl + '" data-vip="' + data[i].isVIP + '" data-mechanics="' + data[i].mechanics + '" data-nexpr="' + data[i].noExpiry + '" data-title="' + data[i].title + '"  data-tmc="' + data[i].tmc + '" data-validity="' + data[i].validity + '"   data-vid="' + data[i].vendorID + '"  >' + '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' + '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + "</svg>" + "<span >Edit</span>" + " </a>" + '<a class="tbl-delete"  data-id="' + data[i].id + '" >' + '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' + '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' + 'fill="black"/>' + "</svg>" + "<span>Delete</span></a>" + "</div></div>";
          $("#privilege-table")
            .dataTable()
            .fnAddData(["<td>" + tdbuttons + "</td>", "<td><p>" + data[i].description + "</p></td>", "<td><p>" + data[i].validity + "</p></td>", "<td><p>" + noExpiry + "</p></td>", "<td><p>" + data[i].vendorName + "</p></td>", "<td><p>" + isVIP + "</p></td>", "<td><p>" + data[i].businessTypeName + "</p></td>", "<td><p>" + data[i].dateCreated + "</p></td>"]);
        }
        $.unblockUI();
      })
      .fail(function () {
        alert("There was an Error When Loading Data...");
      });
  }, 100);
}
async function ShowMembershipList() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Corporate/GetMembershipList", data: {}, type: "GET", datatype: "json" })
      .done(function (data) {
        // 
        membershiptable.clear().draw();
        for (var i = 0; i < data.length; i++) {
          var tdbuttons = "<div>" + "<p>" + data[i].membershipID + "</p>" + "<p>" + data[i].membershipName + "</p>" + '<div class="actions"><a class="tbl-edit" data-id="' + data[i].id + '"  data-memname="' + data[i].membershipName + '" data-descrip="' + data[i].description + '" data-dstart="' + data[i].dateStarted + '" data-dend="' + data[i].dateEnded + '" data-ucount="' + data[i].userCount + '" data-vcount="' + data[i].vipCount + '" >' + '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' + '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + "</svg>" + "<span >Edit</span>" + " </a>" + '<a class="tbl-delete"  data-id="' + data[i].id + '" >' + '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' + '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' + 'fill="black"/>' + "</svg>" + "<span>Delete</span></a>" + '<a class="tbl-view"  data-id="' + data[i].id + '" data-ucount="' + data[i].userCount + '"  data-vcount="' + data[i].vipCount + '"    data-memname="' + data[i].membershipName + '" data-memid="' + data[i].membershipID + '">' + '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="black" class="bi bi-eye" viewBox="0 0 16 16"> <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z"/> <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z"/> </svg>' + "<span>View</span></a>" + "</div></div>";
          $("#membership-table")
            .dataTable()
            .fnAddData(["<td>" + tdbuttons + "</td>", "<td><p>" + data[i].description + "</p></td>", "<td><p>" + data[i].dateStarted + " </p></td>", "<td><p>" + data[i].dateEnded + "</p></td>", "<td><p>" + data[i].userCount + "</p></td>", "<td><p>" + data[i].vipCount + "</p></td>", "<td><p>" + data[i].dateCreated + "</p></td>"]);
        }
        $.unblockUI();
      })
      .fail(function () {
        alert("There was an Error When Loading Data...");
      });
  }, 100);
}

// function CheckedRows() {
//     IdList = [];
//     $('.check-all:checked').each(function (id, item) {

//       IdList.push($(item).data('id'));
//       var domain = {
//         PrivilegeID: $(item).data('id'),
//         Status:$(item).data('status')

//       }

//     });
//     return IdList;
// }
async function ShowVendorDetails() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
      $.ajax({
          url: "/Vendor/GetVendorList",
          data: {
          },
          type: "GET",
          datatype: "json",
          deferRender:true
      })
      .done(function (data) {
        // 
        vbtable.clear().draw();
        for (var i = 0; i < data.length; i++) {
          var img = data[i].vendorLogo;
          if (img == "") {
            img = "https://www.alfardanoysterprivilegeclub.com/assets/img/nophotos.JPG";
          } else {
            img = data[i].vendorLogo;
          }
          var tdbuttons = ' <div class="data-img2">' +
              '<img src=' + img + ' alt=' + data[i].vendorName + ' width="100%" />' +
          '</div>' +
          '<div>' +
              '<p>' + data[i].vendorID + '</p>' +
              '<p>' + data[i].vendorName + '</p>' +
              '<div class="actions"><a class="tbl-edit"data-id="' + data[i].id
              + '"  data-vendorname="' + data[i].vendorName
              + '"   data-descript="' + data[i].description
              + '"  data-serv="' + data[i].services
              + '" data-weburl="' + data[i].websiteUrl
              + '" data-cno="' + data[i].cno + '" data-email="'
              + data[i].email + '" data-btid="'
              + data[i].btypeID + '" data-address="'
              + data[i].address + '"  data-bid="'
              + data[i].bid + '"  data-image="'
              + data[i].featureImg + '"  data-logo="'
              + data[i].vendorLogo + '">' +
          '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' +
          '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' +
          'stroke="black"' +
          'stroke-linecap="round"' +
          'stroke-linejoin="round"/>' +
          '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' +
          'stroke="black"' +
          'stroke-linecap="round"' +
          'stroke-linejoin="round"/>' +
          '</svg>' +

          '<span >Edit</span>' +
          ' </a>' +
          '<a class="tbl-delete"  data-id="' + data[i].id + '" >' +
          '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' +
          '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' +
          'fill="black"/>' +
          '</svg>' +
          '<span>Delete</span></a>' +
          '</div></div>';
          $("#vendor-table").dataTable().fnAddData([
            "<td>" + tdbuttons + "</td>",
            "<td><p>" + data[i].description + "</p></td>",
            "<td><p>" + data[i].services + "</p></td>",
            "<td><p>" + data[i].websiteUrl + "</p></td>",
            "<td><p>" + data[i].email + "</p></td>",
            "<td><p>" + data[i].businessName + "</p></td>",
            "<td><p>" + data[i].vendorlocation + "</p></td>",
            // "<td></td><img src=" + data[i].featureImg + " alt=" + data[i].vendorName + ' width="100%" style="background-color:#979699" /></div></td>',
            "<td><p>" + data[i].status + "</p></td>",
            "<td><p>" + data[i].dateCreated + "</p></td>"]);
        }
        $.unblockUI();
      })
      .fail(function () {
        alert("There was an Error When Loading Data...");
      });
  }, 100);
}
async function ShowOfferingList() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({
      url: '/Offering/GetOfferingList',
      data: {

      },
      type: "GET",
      datatype: "json"
    }).done(function (data) {
      offeringtable.clear().draw();
      // 
      for (var i = 0; i < data.length; i++) {

          var tdbuttons = '<label for="cb"><input type="checkbox" hidden class="cbox" data-id="' + data[i].id+ '"></label>' +
              ' <div class="data-img">' +
          '<img src=' + data[i].imgUrl + ' alt=' + data[i].offeringName + ' width="100%" />' +
          '</div>' +
          '<div>' +
          '<p>' + data[i].offeringID + '</p>' +
          '<p>' + data[i].offeringName + '</p>' +
          '<div class="actions"><a class="tbl-edit" data-id="' + data[i].id + '" data-btypeid="' + data[i].businessTypeID + '" data-edt="' + data[i].endDateTime + '" data-ft="' + data[i].fromTime + '" data-imgurl="' + data[i].imgUrl + '" data-memid="' + data[i].membershipID
          + '" data-offd="' + data[i].offerdays
          + '" data-ofname="' + data[i].offeringName
          + '" data-promdes="' + data[i].promoDesc
          + '" data-sdt="' + data[i].startDateTime
          + '" data-tt="' + data[i].toTime
          + '" data-vid="' + data[i].vendorID
          + '" data-url="' + data[i].url + '">' +
          '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' +
          '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' +
          'stroke="black"' +
          'stroke-linecap="round"' +
          'stroke-linejoin="round"/>' +
          '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' +
          'stroke="black"' +
          'stroke-linecap="round"' +
          'stroke-linejoin="round"/>' +
          '</svg>' +

          '<span >Edit</span>' +
          ' </a>' +
          '<a class="tbl-delete"  data-id="' + data[i].id + '" >' +
          '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' +
          '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' +
          'fill="black"/>' +
          '</svg>' +
           '<span>Delete</span></a>' +
              '<a class="tbl-sendemail"  data-id="' + data[i].id + '" >' + '<svg width="11" height="10" viewBox="0 0 20 14" fill="none" xmlns="http://www.w3.org/2000/svg">' + '<path d="M0 12.3047V1.19576C0 1.1829 0.0192864 1.06075 0.0578592 0.829315L6.59595 6.42237L0.0771456 12.6905C0.0257152 12.5104 0 12.3819 0 12.3047V12.3047ZM0.867888 0.0578592C0.957891 0.0192864 1.06718 0 1.19576 0H18.8042C18.92 0 19.0357 0.0192864 19.1514 0.0578592L12.594 5.6702L11.7261 6.36451L10.0096 7.77242L8.29315 6.36451L7.42526 5.6702L0.867888 0.0578592ZM0.887174 13.4426L7.46384 7.13597L10.0096 9.19961L12.5554 7.13597L19.1321 13.4426C19.0293 13.4812 18.92 13.5005 18.8042 13.5005H1.19576C1.0929 13.5005 0.990035 13.4812 0.887174 13.4426V13.4426ZM13.4233 6.42237L19.9421 0.829315C19.9807 0.945034 20 1.06718 20 1.19576V12.3047C20 12.4204 19.9807 12.549 19.9421 12.6905L13.4233 6.42237Z" fill="black"/>' + "</svg>" +
              "<span>Send Email</span></a>" + 
          '</div></div>';
        $('#offering-table').dataTable().fnAddData([

          '<td>' + tdbuttons + '</td>',
          '<td><p>' + data[i].promoDesc + '</p></td>',
          // '<td><p>' + data[i].promoReleaseText+'</p></td>',
          '<td><p>' + data[i].vendorName + '</p></td>',
          '<td><p>' + data[i].businessTypeName + '</p></td>',
          '<td><p>' + data[i].membershipName + '</p></td>',
          '<td><p>' + data[i].dateCreated + '</p></td>',



        ]);
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });

  }, 100);

}
async function ShowPositionList() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({
      url: "/Register/GetPosition",
      data: {
      },
      type: "GET",
      datatype: "json"
    }).done(function (data) {
        console.log(data);
      postable.clear().draw();
      for (var i = 0; i < data.length; i++) {

        var tdbuttons = "<div>" + "<p>" + data[i].positionID + "</p>" + "<p>" + data[i].positionName + "</p>" +
          '<div class="actions"><a class="tbl-edit" data-id="' + data[i].id + '" data-posname="' + data[i].positionName + '"  data-description="' + data[i].description + '"   >' + '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' + '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + "</svg>" + "<span >Edit</span>" + " </a>"
          + '<a class="tbl-delete"  data-id="' + data[i].id + '" >' + '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' + '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' + 'fill="black"/>' + "</svg>" + "<span>Delete</span></a>" + "</div></div>";
        $("#pos-table").dataTable().fnAddData([
          "<td>" + tdbuttons + "</td>",
          "<td><p>" + data[i].description + "</p></td>",
          "<td><p>" + data[i].dateCreated + "</p></td>"]);
      }
      $.unblockUI();
    })
      .fail(function () {
        alert("There was an Error When Loading Data...");
      });
  }, 100);
}
async function ShowBusinessTypeList() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({
      url: '/Business/GetBusinessTypeList',
      data: {

      },
      type: "GET",
      datatype: "json"
    }).done(function (data) {
      //// 
      btypetable.clear().draw();
      for (var i = 0; i < data.length; i++) {

        var vip = "";
        if (data[i].isVIP == 0) {
          vip = "NOT VIP";
        }
        else {
          vip = "VIP";
        }
        var tdbuttons = ' <div class="data-img">' +
          '<img src=' + data[i].imgURL + ' alt=' + data[i].businessTypeName + ' width="100%" />' +
          '</div>' +
          '<div>' +
          '<p>' + data[i].businessTypeID + '</p>' +
          '<p>' + data[i].businessTypeName + '</p>' +
          '<div class="actions"><a class="tbl-edit" data-id="' + data[i].id
          + '" data-btypename="' + data[i].businessTypeName
          + '" data-promo="' + data[i].promoText
          + '" data-imgurl="' + data[i].imgURL
          + '" data-vip="' + data[i].isVIP
          + '" data-descrip="' + data[i].description + '">' +
          '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' +
          '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' +
          'stroke="black"' +
          'stroke-linecap="round"' +
          'stroke-linejoin="round"/>' +
          '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' +
          'stroke="black"' +
          'stroke-linecap="round"' +
          'stroke-linejoin="round"/>' +
          '</svg>' +

          '<span >Edit</span>' +
          ' </a>' +
          '<a class="tbl-delete"  data-id="' + data[i].id + '" >' +
          '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' +
          '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' +
          'fill="black"/>' +
          '</svg>' +
          '<span>Delete</span></a>' +
          '</div></div>';
        $('#btype-table ').dataTable().fnAddData([

          '<td>' + tdbuttons + '</td>',
          '<td><p>' + data[i].description + '</p></td>',
          // '<td><p>' + data[i].promoReleaseText+'</p></td>',
          '<td><p>' + data[i].promoText + '</p></td>',
          '<td><p>' + vip + '</p></td>',
          '<td><p>' + data[i].dateCreated + '</p></td>',
        ]);
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });

  }, 100);
}

async function ShowBusinesslocDetails() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({
      url: '/Business/GetBusLoc',
      data: {

      },
      type: "GET",
      datatype: "json"
    }).done(function (data) {
      //// 
      dbtable.clear().draw();
      for (var i = 0; i < data.length; i++) {

        var tdbuttons = "<div>" + "<p>" + data[i].businessLocID + "</p>" + "<p>" + data[i].city + "</p>" +
          '<div class="actions">' +
          '<a class="tbl-edit" data-id="' + data[i].id + '" data-country="' + data[i].country + '" data-pc="' + data[i].postalCode + '" data-city="' + data[i].city + '"   >' + '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' + '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + "</svg>" + "<span >Edit</span>" + " </a>"
          + '<a class="tbl-delete"  data-id="' + data[i].id + '" >' + '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' + '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' + 'fill="black"/>' + "</svg>" + "<span>Delete</span></a>" + "</div></div>";
        $("#businessloc-table").dataTable().fnAddData([
          "<td>" + tdbuttons + "</td>",
          "<td><p>" + data[i].postalCode + "</p></td>",
          "<td><p>" + data[i].country + "</p></td>",
          "<td><p>" + data[i].dateCreated + "</p></td>",

        ]);
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });

  }, 100);
}
async function ShowGetCallToActionsModal() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetCallToActionModal",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                 
                nctable.clear().draw();
                for (var i = 0; i < data.length; i++) {
                    $("#n_call").dataTable().fnAddData([
                        "<td><p></p><p>" + data[i].business + "</p></td>",
                        "<td><p>" + data[i].category + "</p></td>",
                        "<td><p>" + data[i].callCount + "</p></td>",
                        "<td><p>" + data[i].emailCount + "</p></td>",
                        "<td><p>" + data[i].bookCount + "</p></td>",

                    ]);
                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowMostClickStoreList() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetMostClickStoreAll",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                 
                nctable.clear().draw();
                var sum = 0;
                for (var x = 3; x < data.length; x++) {
                    sum += data[x].total;
                   
                } 
                //console.log(sum);
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("mcs_top1").innerHTML = "Others";
                    document.getElementById("mcs_perc1").innerHTML = sum.toFixed(2) + " %";

                    document.getElementById("mcs_top2").innerHTML = data[0].business;
                    document.getElementById("mcs_perc2").innerHTML = data[0].total + " %";

                    document.getElementById("mcs_top3").innerHTML = data[1].business;
                    document.getElementById("mcs_perc3").innerHTML = data[1].total + " %";

                    document.getElementById("mcs_top4").innerHTML = data[2].business;
                    document.getElementById("mcs_perc4").innerHTML = data[2].total + " %";
                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowGetMostClickStoreAll() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetMostClickStoreAll",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                 
                mcstable.clear().draw();
                for (var i = 0; i < data.length; i++) {
                    $("#mcs_call").dataTable().fnAddData([
                        "<td><p></p><p>" + data[i].business + "</p></td>",
                        "<td><p>" + data[i].count + "</p></td>",
                        "<td><p>" + data[i].total + "</p></td>",

                    ]);
                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowGetMostClickedHospitalityList() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetMostClickedHospitalityAll",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                 
                nctable.clear().draw();
                var sum = 0;
                for (var x = 3; x < data.length; x++) {
                    sum += data[x].total;
                    
                } 
                //console.log(sum);
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("mch_top1").innerHTML = "Others";
                    document.getElementById("mch_perc1").innerHTML = sum.toFixed(2) + " %";

                    document.getElementById("mch_top2").innerHTML = data[0].business;
                    document.getElementById("mch_perc2").innerHTML = data[0].total + " %";

                    document.getElementById("mch_top3").innerHTML = data[1].business;
                    document.getElementById("mch_perc3").innerHTML = data[1].total + " %";

                    document.getElementById("mch_top4").innerHTML = data[2].business;
                    document.getElementById("mch_perc4").innerHTML = data[2].total + " %";
                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}

async function ShowShowGetMostClickModal() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetMostClickedHospitalityAll",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                 
                mchtable.clear().draw();
                for (var i = 0; i < data.length; i++) {
                
                    $("#mch_call").dataTable().fnAddData([
                        "<td><p></p><p>" + data[i].business + "</p></td>",
                        "<td><p>" + data[i].count + "</p></td>",
                        "<td><p>" + data[i].total + "</p></td>",

                    ]);

                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowGetMostClickRestaurant() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetMostClickRestaurantAll",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                 
                mcrtable.clear().draw();
                var sum = 0;
                for (var x = 3; x < data.length; x++) {
                    sum += data[x].total;
                    
                }
                //console.log(sum);
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("mcr_top1").innerHTML = "Others";
                    document.getElementById("mcr_perc1").innerHTML = sum.toFixed(2) + " %";

                    document.getElementById("mcr_top2").innerHTML = data[0].business;
                    document.getElementById("mcr_perc2").innerHTML = data[0].total + " %";

                    document.getElementById("mcr_top3").innerHTML = data[1].business;
                    document.getElementById("mcr_perc3").innerHTML = data[1].total + " %";

                    document.getElementById("mcr_top4").innerHTML = data[2].business;
                    document.getElementById("mcr_perc4").innerHTML = data[2].total + " %";
                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowMostClickRestaurantAllkModal() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetMostClickRestaurantAll",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                 
                mcrtable.clear().draw();
                for (var i = 0; i < data.length; i++) {

                    $("#mcr_call").dataTable().fnAddData([
                        "<td><p></p><p>" + data[i].business + "</p></td>",
                        "<td><p>" + data[i].count + "</p></td>",
                        "<td><p>" + data[i].total + "</p></td>",

                    ]);

                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowAllUserCount() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetCountAllUser",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                 
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("all-user").innerHTML = data[i].count;
                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowSupportcount() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetSuppoprtCount",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                // 
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("supcount").innerHTML = data[i].supportcount;
                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowFilteredNewRegistered() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetNewRegisteredWeekly",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("new_user").innerHTML = data[i].count;
                    document.getElementById("percent-registered").innerHTML = data[i].percentage.toFixed(2) + " %";
                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function GetClickCountTop2() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetClickCounts",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                // 
                for (var i = 0; i < data.length; i++) {

                    $("#tbl_cnt tbody").append([
                        "<tr>" +
                        "<td>" + data[i].module + "</td>" +
                        "<td>" + data[i].count + "</td>", + "</tr>"
                    ]);

                }

                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function GetClickCountAll() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetClickCountsGetAll",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                 
                nbtable.clear().draw();
                for (var i = 0; i < data.length; i++) {

                    $("#n_clicktbl").dataTable().fnAddData([
                        "<td><p></p><p>" + data[i].module + "</p></td>",
                        "<td><p>" + data[i].count + "</p></td>",

                    ]);
                }

                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowGetCallToActions() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetCallToAction",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                 
                cAbtable.clear().draw();
                for (var i = 0; i < data.length; i++) {
                    $("#cAction_table").dataTable().fnAddData([
                        "<td><p></p><p>" + data[i].business + "</p></td>",
                        "<td><p>" + data[i].category + "</p></td>",
                        "<td><p>" + data[i].callCount + "</p></td>",
                        "<td><p>" + data[i].emailCount + "</p></td>",
                        "<td><p>" + data[i].bookCount + "</p></td>",

                    ]);
                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowUserListModal() {
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Offering/GetUserList",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                console.log(data);
                oul_table.clear().draw();
                for (var i = 0; i < data.length; i++) {
                    $("#oul_call").dataTable().fnAddData([
                        "<td><label for=cb><input type=checkbox  class=cbox1 data-email= "+ data[i].email+ " >'"+
                        '</label > <p>'+ data[i].fullname + '</p></td >',
                        "<td><p></p><p>" + data[i].email + "</p></td>",
                    ]);
                }
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowGetLineGraphCountList() {

    const ctx = document.getElementById("myChart").getContext("2d");
    const arrdate = new Array();
    const arrval = new Array();
    console.log($('#nur').val());
    $.blockUI(reloadLoading);
    setTimeout(function () {
        $.ajax(
            {
                url: "/Dashboard/GetLineGraphCount",
                data: {

                },
                type: "GET",
                datatype: "json"
            }).done(function (data) {
                for (var i = 0; i < data.length; i++) {

                    arrdate.push(data[i].dateCreated);
                    arrval.push(data[i].count);
                }
                //console.log(arrval);
                var chartData = {
                    labels: arrdate, // conditions to made
                    datasets: [
                        {
                            label: "",
                            data: arrval,
                            backgroundColor: "rgba(255, 99, 132, 0.2)",
                            borderColor: "rgba(255, 99, 132, 1)",
                            borderWidth: 1,
                        },
                    ],
                };
               
                var chartOptions = {
                    responsive: true,
                    maintainAspectRatio: false,

                    scales: {
                        yAxes: [
                            {
                                ticks: {
                                    beginAtZero: true,
                                },
                            },
                        ],
                    },
                    plugins: {
                        legend: {
                            display: false
                        }
                    }

                };
                    // Create the chart instance
                    myChart.destroy();
                    myChart = new Chart(ctx, {
                        type: "line",
                        data: chartData,
                        options: chartOptions,
                    });

                    // Update the chart data
                    setInterval(function () {

                        myChart.update();
                    }, 5000);

                
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}