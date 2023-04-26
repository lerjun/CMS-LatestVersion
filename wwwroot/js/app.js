let darkmode = window.localStorage.getItem("darkmode");
let toggle = document.querySelector("#darkmode");

if (darkmode === "true") {
    document.body.classList.add("darkmode");
    toggle.checked = true;
}

toggle.addEventListener("click", function () {
    darkmode = localStorage.getItem("darkmode");
    if (darkmode !== "true") {
        document.body.classList.add("darkmode");
        document.getElementById("logo").src = "/img/AOPC-White.png";
        window.localStorage.setItem("darkmode", "true");
        toggle.innerText = "Light";
    } else {
        document.body.classList.remove("darkmode");
        window.localStorage.setItem("darkmode", "false");
        document.getElementById("logo").src = "/img/AOPC-Black.png";
        toggle.innerText = "Dark";
    }
});

// Top User Graph, Please 4 data only and make sure that the data value are converted to percentage
async function ShowPiegraphMostClickRestaurant() {
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
                var topBusinesOutlet = "";
                var sum = 0;
                for (var x = 3; x < data.length; x++)
                {
                    sum += data[x].total;
                 
                } //console.log(sum);
                for (var i = 0; i < data.length; i++) {
             
                    topBusinesOutlet  = [
                        {
                            name:"Others",
                            totalVisit: sum, //percentage
                        },
                        {
                            name: data[0].business,
                            totalVisit: data[0].total, //percentage
                        },
                        {
                            name: data[1].business,
                            totalVisit: data[1].total,  //percentage
                        },
                        {
                            name: data[2].business,
                            totalVisit: data[2].total,  //percentage
                        },
                    ];
                }

                    const pieGraphMovement = (elementContainer, data) => {
                        let oldValue = 0;
                        let styleString = "";
                        let startingPoint = "";
                        const color = {
                            0: " var(--accent)",
                            1: " var(--primary)",
                            2: " var(--secondary-brown)",
                            3: " var(--secondary-light)",
                        };
                        data.map((value, key) => {
                            startingPoint = oldValue == 0 ? "" : `${oldValue}%`;
                            let newValue = value.totalVisit + oldValue;
                            styleString +=
                                oldValue == 0
                                    ? `${color[key]} ${startingPoint} ${value.totalVisit}%`
                                    : `,${color[key]} ${startingPoint} ${newValue}%`;
                            oldValue += value.totalVisit;
                        });
                      //  console.log(styleString);
                        document
                            .querySelector(`${elementContainer}`)
                            .setAttribute("style", `background: conic-gradient(${styleString})`);
                    };

                    const populateInfo = (elementContainer, data) => {
                        let element = document.querySelector(`${elementContainer}`);
                        data.map((value, key) => {
                            // console.log();
                            element.children[key].children[1].innerText = value.name;
                            element.children[key].children[2].innerText = `${value.totalVisit}%`;
                        });
                    };

                    const populateTable = (elementContainer, data) => {
                        let element = document
                            .querySelector(`${elementContainer}`)
                            .children[0].getElementsByTagName("tbody");
                        data.map((value, key) => {
                            if (value.name !== "Others") {
                                let newRow = element[0].insertRow();
                                let nameCell = newRow.insertCell();
                                let valueCell = newRow.insertCell();
                                var nameNode = document.createTextNode(value.name);
                                var valueNode = document.createTextNode(value.totalVisit);
                                nameCell.appendChild(nameNode);
                                valueCell.appendChild(valueNode);
                            }
                        });
                    };

                    pieGraphMovement("#to-pie-graph", topBusinesOutlet);
                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowPiegraphMostClickHospitality() {
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
                 
                mcrtable.clear().draw();
                var topBusinesOutlet = "";
                var sum = 0;
                for (var x = 3; x < data.length; x++) {
                    sum += data[x].total;
                   // console.log(sum +"---");
                } 
                for (var i = 0; i < data.length; i++) {

                    topBusinesOutlet = [
                        {
                            name: "Others",
                            totalVisit: sum, //percentage
                        },
                        {
                            name: data[0].business,
                            totalVisit: data[0].total, //percentage
                        },
                        {
                            name: data[1].business,
                            totalVisit: data[1].total,  //percentage
                        },
                        {
                            name: data[2].business,
                            totalVisit: data[2].total,  //percentage
                        },
                    ];
                }

                const pieGraphMovement = (elementContainer, data) => {
                    let oldValue = 0;
                    let styleString = "";
                    let startingPoint = "";
                    const color = {
                        0: " var(--accent)",
                        1: " var(--primary)",
                        2: " var(--secondary-brown)",
                        3: " var(--secondary-light)",
                    };
                    data.map((value, key) => {
                        startingPoint = oldValue == 0 ? "" : `${oldValue}%`;
                        let newValue = value.totalVisit + oldValue;
                        styleString +=
                            oldValue == 0
                                ? `${color[key]} ${startingPoint} ${value.totalVisit}%`
                                : `,${color[key]} ${startingPoint} ${newValue}%`;
                        oldValue += value.totalVisit;
                    });
                    //console.log(styleString);
                    document
                        .querySelector(`${elementContainer}`)
                        .setAttribute("style", `background: conic-gradient(${styleString})`);
                };

                const populateInfo = (elementContainer, data) => {
                    let element = document.querySelector(`${elementContainer}`);
                    data.map((value, key) => {
                        // console.log();
                        element.children[key].children[1].innerText = value.name;
                        element.children[key].children[2].innerText = `${value.totalVisit}%`;
                    });
                };

                const populateTable = (elementContainer, data) => {
                    let element = document
                        .querySelector(`${elementContainer}`)
                        .children[0].getElementsByTagName("tbody");
                    data.map((value, key) => {
                        if (value.name !== "Others") {
                            let newRow = element[0].insertRow();
                            let nameCell = newRow.insertCell();
                            let valueCell = newRow.insertCell();
                            var nameNode = document.createTextNode(value.name);
                            var valueNode = document.createTextNode(value.totalVisit);
                            nameCell.appendChild(nameNode);
                            valueCell.appendChild(valueNode);
                        }
                    });
                };

                pieGraphMovement("#th-pie-graph", topBusinesOutlet);

                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}
async function ShowPiegraphMostClickStore() {
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
                 
                mcrtable.clear().draw();
                var topBusinesOutlet = "";
                var sum = 0;
                for (var x = 3; x < data.length; x++) {
                    sum += data[x].total;
                   // console.log(sum + "---");
                }
                for (var i = 0; i < data.length; i++) {

                    topBusinesOutlet = [
                        {
                            name: "Others",
                            totalVisit: sum, //percentage
                        },
                        {
                            name: data[0].business,
                            totalVisit: data[0].total, //percentage
                        },
                        {
                            name: data[1].business,
                            totalVisit: data[1].total,  //percentage
                        },
                        {
                            name: data[2].business,
                            totalVisit: data[2].total,  //percentage
                        },
                    ];
                }

                const pieGraphMovement = (elementContainer, data) => {
                    let oldValue = 0;
                    let styleString = "";
                    let startingPoint = "";
                    const color = {
                        0: " var(--accent)",
                        1: " var(--primary)",
                        2: " var(--secondary-brown)",
                        3: " var(--secondary-light)",
                    };
                    data.map((value, key) => {
                        startingPoint = oldValue == 0 ? "" : `${oldValue}%`;
                        let newValue = value.totalVisit + oldValue;
                        styleString +=
                            oldValue == 0
                                ? `${color[key]} ${startingPoint} ${value.totalVisit}%`
                                : `,${color[key]} ${startingPoint} ${newValue}%`;
                        oldValue += value.totalVisit;
                    });
                   // console.log(styleString);
                    document
                        .querySelector(`${elementContainer}`)
                        .setAttribute("style", `background: conic-gradient(${styleString})`);
                };

                const populateInfo = (elementContainer, data) => {
                    let element = document.querySelector(`${elementContainer}`);
                    data.map((value, key) => {
                        // console.log();
                        element.children[key].children[1].innerText = value.name;
                        element.children[key].children[2].innerText = `${value.totalVisit}%`;
                    });
                };

                const populateTable = (elementContainer, data) => {
                    let element = document
                        .querySelector(`${elementContainer}`)
                        .children[0].getElementsByTagName("tbody");
                    data.map((value, key) => {
                        if (value.name !== "Others") {
                            let newRow = element[0].insertRow();
                            let nameCell = newRow.insertCell();
                            let valueCell = newRow.insertCell();
                            var nameNode = document.createTextNode(value.name);
                            var valueNode = document.createTextNode(value.totalVisit);
                            nameCell.appendChild(nameNode);
                            valueCell.appendChild(valueNode);
                        }
                    });
                };

                pieGraphMovement("#ts-pie-graph", topBusinesOutlet);

                $.unblockUI();
            }).fail(function () {
                alert("There was an Error When Loading Data...");
            });
    }, 100);
}

// fetch("http://192.168.100.90/api/ApiBusiness/BusinessList", {
// method: "GET",
// headers: {
//     "Content-Type": "application/json",
//     Authorization:
//       "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiYXciLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3ZlcnNpb24iOiJWMy41IiwibmJmIjoxNjcyNjY2NjkwLCJleHAiOjE2ODEzMDY2OTAsImlhdCI6MTY3MjY2NjY5MH0.Pr9t21HPo8NkVNQRN5PMVK9mLx_Zoc-daVNPcCls3oE",
// },
// })
// .then((response) => {
//     console.log(response.json());
// })
// .then((data) => {
//     //handle data
//     // 
// })
// .catch((error) => {
//     console.log(error);
// });
async function ShowOptBtype() {
  $.ajax({ url: "/Business/GetBusinessTypeList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
    // 
    $("#btype-option").empty();
    $("#btype-option").append('<option value="0">-Select Business Type-</option>');
    for (var i = 0; i < data.length; i++) {
      $("#btype-option").append('<option value="' + data[i].id + '">' + data[i].businessTypeName + "</option>");
    }
  });
}
async function ShowprivOpt() {
  $.ajax({ url: "/MembershipPrivilege/GetPrivilegeList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
    // 
    $("#privilege-option").empty();
    $("#privilege-option").append('<option value="0">-Select Privilege-</option>');
    for (var i = 0; i < data.length; i++) {
      $("#privilege-option").append('<option value="' + data[i].id + '">' + data[i].title + "</option>");
    }
  });
}
async function ShowVendorOpt() {
  $.ajax({ url: "/Vendor/GetVendorList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
    // 
    $("#vendor-option").empty();
    $("#vendor-option").append('<option value="0">-Select Vendor-</option>');
    for (var i = 0; i < data.length; i++) {
      $("#vendor-option").append('<option value="' + data[i].id + '">' + data[i].vendorName + "</option>");
    }
  });
}
async function ShowPositionName() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Register/GetPosition", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      $("#position").empty();
      $("#position").append('<option value="0">-Select Option-</option>');
      for (var i = 0; i < data.length; i++) {
        $("#position").append('<option value="' + data[i].id + '">' + data[i].name + "</option>");
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function ShowPositionName() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Register/GetPosition", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      $("#corp-position").empty();
      $("#corp-position").append('<option value="0">-Select Option-</option>');
      for (var i = 0; i < data.length; i++) {
        $("#corp-position").append('<option value="' + data[i].id + '">' + data[i].positionName + "</option>");
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function UserShowPositionName() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax(
      {
        url: "/Register/GetPosition",
        data: {

        },
        type: "GET",
        datatype: "json"
      }).done(function (data) {
        // 
        $("#user-position").empty();
        $("#user-position").append('<option value="0">-Select Option-</option>');
        for (var i = 0; i < data.length; i++) {
          $("#user-position").append('<option value="' + data[i].id + '">' + data[i].positionName + "</option>");
        }
        $.unblockUI();
      }).fail(function () {
        alert("There was an Error When Loading Data...");
      });
  }, 100);
}

async function _ShowMembershipOption() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Corporate/GetMembershipList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      // 
      $("#memtier-option").empty();
      $("#memtier-option").append('<option value="0">-Select Tier-</option>');
      // $("#memtier-option").append('<option value="ALL">-Select All-</option>');
      for (var i = 0; i < data.length; i++) {
        $("#memtier-option").append('<option value="' + data[i].id + '">' + data[i].membershipName + "</option>");
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function ShowMembershipOption() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Corporate/GetMembershipList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      // 
      $("#mem-option").empty();
      $("#mem-option").append('<option value="0">-Select Tier-</option>');
      for (var i = 0; i < data.length; i++) {
        $("#mem-option").append('<option value="' + data[i].id + '">' + data[i].membershipName + "</option>");
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function ShowCorporateOption() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Corporate/GetCorporateList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      console.log("aw");
      ("");
      $("#corporate-option").empty();
      $("#corporate-option").append('<option value="0">-Select Corporate-</option>');
      for (var i = 0; i < data.length; i++) {
        $("#corporate-option").append('<option value="' + data[i].id + '">' + data[i].corporateName + "</option>");
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function ShowUserType() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Register/GetUserType", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      // 
      $("#usertype-option").empty();
      $("#usertype-option").append('<option value="0">-Select User Type-</option>');
      for (var i = 0; i < data.length; i++) {
        $("#usertype-option").append('<option value="' + data[i].id + '">' + data[i].userType + "</option>");
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}

async function ShowOptionMembership() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Corporate/GetMembershipList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      $("#membership-option").empty();
      $("#membership-option").append('<option value="0">-Select Tier-</option>');
      for (var i = 0; i < data.length; i++) {
        $("#membership-option").append('<option value="' + data[i].id + '">' + data[i].membershipName + "</option>");
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function CorpShowOptionMembership() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Corporate/GetMembershipList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      $("#corp-mem-option").empty();
      $("#corp-mem-option").append('<option value="0">-Select Tier-</option>');
      for (var i = 0; i < data.length; i++) {
        $("#corp-mem-option").append('<option value="' + data[i].id + '">' + data[i].membershipName + "</option>");
      }
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function ShowOptBloctype() {
  $.ajax({ url: "/Business/GetBusLoc", data: {}, type: "GET", datatype: "json" }).done(function (data) { // @* //  *@
    $("#btype-option").empty();
    $("#bloc-option").append('<option value="0">-Select Business Location-</option>');
    for (var i = 0; i < data.length; i++) {
      $("#bloc-option").append('<option value="' + data[i].id + '">' + data[i].city + "</option>");
    }
  });
}
async function ShowBusinessAddress() {
  $.ajax({ url: "/Business/GetBusinessList", data: {}, type: "GET", datatype: "json" }).done(function (data) { // @* //  *@
    $("#b-option").empty();
    $("#b-option").append('<option value="0">-Select Business Address-</option>');
    for (var i = 0; i < data.length; i++) {
      $("#b-option").append('<option value="' + data[i].id + '">' + data[i].address + "</option>");
    }
  });
}
async function ShowCorporateOption() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Corporate/GetCorporateList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      $("#user-corporate-option").empty();
      $("#user-corporate-option").append('<option value="0">-Select Corporate-</option>');
      for (var i = 0; i < data.length; i++) {
        $("#user-corporate-option").append('<option value="' + data[i].id + '">' + data[i].corporateName + "</option>");
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function CorpShowCorporateOption() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Corporate/GetCorporateList", data: {}, type: "GET", datatype: "json" }).done(function (data) { // // 
      $("#corp-corporate-option").empty();
      $("#corp-corporate-option").append('<option value="0">-Select Corporate-</option>');
      for (var i = 0; i < data.length; i++) {
        $("#corp-corporate-option").append('<option value="' + data[i].id + '">' + data[i].corporateName + "</option>");
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}

async function ShowUserDetails() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Register/GetuserList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      dbtable.clear().draw();
      // 
      for (var i = 0; i < data.length; i++) {
        var img = data[i].filePath;
        if (img == "") {
          img = "https://www.alfardanoysterprivilegeclub.com/assets/img/defaultavatar.png";
        } else {
          img = data[i].filePath;
        }
        $("#register-table tbody").append([
          "<tr>" + "<td>" + ' <div class="data-img">' + "<img src=" + img + " alt=" + data[i].fname + ' width="100%" />' + "</div>" + "<div>" + "<p>" + data[i].employeeID + "</p>" + "<p>" + data[i].fname + " " + data[i].lname + "</p>" + '<div class="actions"><a class="tbl-edit" data-id="' + data[i].id + '" data-username="' + data[i].username + '" data-employeeid="' + data[i].employeeID + '" data-fname="' + data[i].fname + '" data-lname="' + data[i].lname + '" data-email="' + data[i].email + '" data-pos="' + data[i].positionID + '"  data-gender="' + data[i].gender + '" data-utype="' + data[i].userType + '" data-usercorpid="' + data[i].corporateID + '"  data-displayimg="' + data[i].filePath + '"  data-vipstats="' + data[i].isVIP + '">' + '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' + '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + "</svg>" + "<span >Edit</span>" + " </a>" + '<a class="tbl-delete" data-id="' + data[i].id + '"  data-username="' + data[i].username + '" data-employeeid="' + data[i].employeeID + '" data-fname="' + data[i].fname + '" data-lname="' + data[i].lname + '" data-email="' + data[i].email + '" data-pos="' + data[i].positionID + '"  data-gender="' + data[i].gender + '" >' + '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' + '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' + 'fill="black"/>' + "</svg>" + "<span>Delete</span></a>" + '<a class="tbl-sendemail" data-id="' + data[i].id + '"  data-username="' + data[i].username + '" data-employeeid="' + data[i].employeeID + '" data-fname="' + data[i].fname + '" data-lname="' + data[i].lname + '" data-sentemail="' + data[i].email + '" data-pos="' + data[i].positionID + '"  data-gender="' + data[i].gender + '"  data-emailstatus="' + data[i].status + '" >' + '<svg width="11" height="10" viewBox="0 0 20 14" fill="none" xmlns="http://www.w3.org/2000/svg">' + '<path d="M0 12.3047V1.19576C0 1.1829 0.0192864 1.06075 0.0578592 0.829315L6.59595 6.42237L0.0771456 12.6905C0.0257152 12.5104 0 12.3819 0 12.3047V12.3047ZM0.867888 0.0578592C0.957891 0.0192864 1.06718 0 1.19576 0H18.8042C18.92 0 19.0357 0.0192864 19.1514 0.0578592L12.594 5.6702L11.7261 6.36451L10.0096 7.77242L8.29315 6.36451L7.42526 5.6702L0.867888 0.0578592ZM0.887174 13.4426L7.46384 7.13597L10.0096 9.19961L12.5554 7.13597L19.1321 13.4426C19.0293 13.4812 18.92 13.5005 18.8042 13.5005H1.19576C1.0929 13.5005 0.990035 13.4812 0.887174 13.4426V13.4426ZM13.4233 6.42237L19.9421 0.829315C19.9807 0.945034 20 1.06718 20 1.19576V12.3047C20 12.4204 19.9807 12.549 19.9421 12.6905L13.4233 6.42237Z" fill="black"/>' + "</svg>" + "<span>Send Email</span></a>" + "</div></div>" + "</td>" + "<td>" + data[i].username + "</td>" + "<td>" + data[i].email + "</td>" + "<td>" + data[i].gender + "</td>" + "<td>" + data[i].corporatename + "</td>" + "<td>" + data[i].position + "</td>" + "<td>" + data[i].status + "</td>" + "<td><p>" + data[i].dateCreated + "</p><p>" + data[i].dateCreated + "</p></td>", + "</tr>"
        ]);
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function ShowUserCorporateList() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Register/GetCorporateUserList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      corusertable.clear().draw();
      // 
      for (var i = 0; i < data.length; i++) {
        var img = data[i].filePath;
        if (img == "") {
          img = "https://www.alfardanoysterprivilegeclub.com/assets/img/defaultavatar.png";
        } else {
          img = data[i].filePath;
        }
        $("#corporate-user-table tbody").append([
          "<tr>" + "<td>" + ' <div class="data-img">' + "<img src=" + img + " alt=" + data[i].fname + ' width="100%" />' + "</div>" + "<div>" + "<p>" + data[i].employeeID + "</p>" + "<p>" + data[i].fname + " " + data[i].lname + "</p>" + '<div class="actions"><a class="corp-tbl-edit" data-id="' + data[i].id + '" data-username="' + data[i].username + '" data-employeeid="' + data[i].employeeID + '" data-fname="' + data[i].fname + '" data-lname="' + data[i].lname + '" data-email="' + data[i].email + '"   data-gender="' + data[i].gender + '"  data-corpdisplayimg="' + data[i].filePath + '" data-corporateid="' + data[i].corporateID + '" data-corporateposition="' + data[i].positionID + '" data-corporategender="' + data[i].gender + '">' + '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' + '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + "</svg>" + "<span >Edit</span>" + " </a>" + '<a class="corp-tbl-delete" data-id="' + data[i].id + '"  data-username="' + data[i].username + '" data-employeeid="' + data[i].employeeID + '" data-fname="' + data[i].fname + '" data-lname="' + data[i].lname + '" data-email="' + data[i].email + '" data-pos="' + data[i].positionID + '"  data-gender="' + data[i].gender + '" >' + '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' + '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' + 'fill="black"/>' + "</svg>" + "<span>Delete</span></a>" + '<a class="corp-tbl-sendemail" data-corporateid="' + data[i].id + '"  data-username="' + data[i].username + '" data-employeeid="' + data[i].employeeID + '" data-fname="' + data[i].fname + '" data-lname="' + data[i].lname + '" data-corpemail="' + data[i].email + '" data-pos="' + data[i].positionID + '"  data-gender="' + data[i].gender + '"  data-corpstatus="' + data[i].status + '"  >' + '<svg width="11" height="10" viewBox="0 0 20 14" fill="none" xmlns="http://www.w3.org/2000/svg">' + '<path d="M0 12.3047V1.19576C0 1.1829 0.0192864 1.06075 0.0578592 0.829315L6.59595 6.42237L0.0771456 12.6905C0.0257152 12.5104 0 12.3819 0 12.3047V12.3047ZM0.867888 0.0578592C0.957891 0.0192864 1.06718 0 1.19576 0H18.8042C18.92 0 19.0357 0.0192864 19.1514 0.0578592L12.594 5.6702L11.7261 6.36451L10.0096 7.77242L8.29315 6.36451L7.42526 5.6702L0.867888 0.0578592ZM0.887174 13.4426L7.46384 7.13597L10.0096 9.19961L12.5554 7.13597L19.1321 13.4426C19.0293 13.4812 18.92 13.5005 18.8042 13.5005H1.19576C1.0929 13.5005 0.990035 13.4812 0.887174 13.4426V13.4426ZM13.4233 6.42237L19.9421 0.829315C19.9807 0.945034 20 1.06718 20 1.19576V12.3047C20 12.4204 19.9807 12.549 19.9421 12.6905L13.4233 6.42237Z" fill="black"/>' + "</svg>" + "<span>Send Email</span></a>" + "</div></div>" + "</td>" + "<td>" + data[i].username + "</td>" + "<td>" + data[i].email + "</td>" + "<td>" + data[i].gender + "</td>" + "<td>" + data[i].corporatename + "</td>" + "<td>" + data[i].position + "</td>" + "<td>" + data[i].status + "</td>" + "<td><p>" + data[i].dateCreated + "</p><p>" + data[i].dateCreated + "</p></td>", + "</tr>"
        ]);
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function ShowUserAdminList() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Register/GetAdminUserList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      adminusertable.clear().draw();
      // 
      for (var i = 0; i < data.length; i++) {
        if (img == "") {
          img = "https://www.alfardanoysterprivilegeclub.com/assets/img/defaultavatar.png";
        } else {
          img = data[i].filePath;
        }
        $("#adminuser-table tbody").append([
          "<tr>" + "<td>" + ' <div class="data-img">' + "<img src=" + img + " alt=" + data[i].fname + ' width="100%" />' + "</div>" + "<div>" + "<p>" + data[i].employeeID + "</p>" + "<p>" + data[i].fname + " " + data[i].lname + "</p>" + '<div class="actions"><a class="admin-tbl-edit" data-adminid="' + data[i].id + '" data-adminusername="' + data[i].username + '" data-adminemployeeid="' + data[i].employeeID + '" data-adminfname="' + data[i].fname + '" data-adminlname="' + data[i].lname + '" data-adminemail="' + data[i].email + '" data-adminpos="' + data[i].positionID + '"  data-admingender="' + data[i].gender + '"  data-admindisplayimg="' + data[i].filePath + '">' + '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' + '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + "</svg>" + "<span >Edit</span>" + " </a>" + '<a class="admin-tbl-delete" data-id="' + data[i].id + '"  data-username="' + data[i].username + '" data-employeeid="' + data[i].employeeID + '" data-fname="' + data[i].fname + '" data-lname="' + data[i].lname + '" data-email="' + data[i].email + '" data-pos="' + data[i].positionID + '"  data-gender="' + data[i].gender + '" >' + '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' + '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' + 'fill="black"/>' + "</svg>" + "<span>Delete</span></a>" + "</div></div>" + "</td>" + "<td>" + data[i].username + "</td>" + "<td>" + data[i].email + "</td>" + "<td>" + data[i].gender + "</td>" + "<td>" + data[i].corporatename + "</td>" + "<td>" + data[i].position + "</td>" + "<td>" + data[i].status + "</td>" + "<td><p>" + data[i].dateCreated + "</p><p>" + data[i].dateCreated + "</p></td>", + "</tr>"
        ]);
      }
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}
async function ShowCorporateDetails() {
  $.blockUI(reloadLoading);
  setTimeout(function () {
    $.ajax({ url: "/Corporate/GetCorporateList", data: {}, type: "GET", datatype: "json" }).done(function (data) {
      // 
      corptable.clear().draw();
      for (var i = 0; i < data.length; i++) {
        $("#_corporate-table tbody").append([
          "<tr>" + "<td>" + "<div>" + "<p>" + data[i].companyID + "</p>" + "<p>" + data[i].corporateName + "</p>" + '<div class="actions"><a class="tbl-edit" data-id="' + data[i].id + '" data-corporatename="' + data[i].corporateName + '"data-address="' + data[i].address + '"  data-cno="' + data[i].cNo + '" data-eadd="' + data[i].emailAddress + '"   data-description="' + data[i].description + '"  data-tier="' + data[i].tier + '"   data-memid="' + data[i].memid + '"   data-ucount="' + data[i].userCount + '" data-vcount="' + data[i].vipCount + '">' + '<svg width="11" height="11" viewBox="0 0 11 11" fill="none"xmlns="http://www.w3.org/2000/svg">' + '<path d="M5.02558 1.92456H1.89457C1.65732 1.92456 1.42978 2.0164 1.26201 2.17986C1.09425 2.34333 1 2.56504 1 2.79621V8.89779C1 9.12896 1.09425 9.35067 1.26201 9.51414C1.42978 9.6776 1.65732 9.76944 1.89457 9.76944H8.15659C8.39385 9.76944 8.62139 9.6776 8.78915 9.51414C8.95692 9.35067 9.05117 9.12896 9.05117 8.89779V5.847"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + '<path d="M8.38023 1.27079C8.55817 1.09741 8.79951 1 9.05116 1C9.30281 1 9.54415 1.09741 9.72209 1.27079C9.90003 1.44417 10 1.67933 10 1.92453C10 2.16973 9.90003 2.40488 9.72209 2.57827L5.47286 6.71862L3.68372 7.15445L4.131 5.41114L8.38023 1.27079Z"' + 'stroke="black"' + 'stroke-linecap="round"' + 'stroke-linejoin="round"/>' + "</svg>" + "<span >Edit</span>" + " </a>" + '<a class="tbl-delete" data-id="' + data[i].id + '" data-corporatename="' + data[i].corporateName + '"data-address="' + data[i].address + '"  data-cno="' + data[i].cNo + '" data-eadd="' + data[i].emailAddress + '"  >' + '<svg width="11" height="10" viewBox="0 0 11 10" fill="none"  xmlns="http://www.w3.org/2000/svg">' + '<path d="M2.56574 1C2.56574 0.734784 2.67387 0.48043 2.86634 0.292893C3.0588 0.105357 3.31985 0 3.59204 0H6.67092C6.94311 0 7.20416 0.105357 7.39663 0.292893C7.58909 0.48043 7.69722 0.734784 7.69722 1V2H9.74981C9.88591 2 10.0164 2.05268 10.1127 2.14645C10.2089 2.24021 10.263 2.36739 10.263 2.5C10.263 2.63261 10.2089 2.75979 10.1127 2.85355C10.0164 2.94732 9.88591 3 9.74981 3H9.20126L8.75636 9.071C8.73793 9.32329 8.62207 9.55941 8.43211 9.73179C8.24215 9.90417 7.99221 10 7.73263 10H2.52982C2.27024 10 2.0203 9.90417 1.83034 9.73179C1.64038 9.55941 1.52452 9.32329 1.50609 9.071L1.06222 3H0.513148C0.377053 3 0.246531 2.94732 0.150298 2.85355C0.0540636 2.75979 0 2.63261 0 2.5C0 2.36739 0.0540636 2.24021 0.150298 2.14645C0.246531 2.05268 0.377053 2 0.513148 2H2.56574V1ZM3.59204 2H6.67092V1H3.59204V2ZM2.09056 3L2.53033 9H7.73314L8.17291 3H2.09056ZM4.10518 4C4.24128 4 4.3718 4.05268 4.46803 4.14645C4.56427 4.24021 4.61833 4.36739 4.61833 4.5V7.5C4.61833 7.63261 4.56427 7.75979 4.46803 7.85355C4.3718 7.94732 4.24128 8 4.10518 8C3.96909 8 3.83857 7.94732 3.74233 7.85355C3.6461 7.75979 3.59204 7.63261 3.59204 7.5V4.5C3.59204 4.36739 3.6461 4.24021 3.74233 4.14645C3.83857 4.05268 3.96909 4 4.10518 4ZM6.15778 4C6.29387 4 6.42439 4.05268 6.52063 4.14645C6.61686 4.24021 6.67092 4.36739 6.67092 4.5V7.5C6.67092 7.63261 6.61686 7.75979 6.52063 7.85355C6.42439 7.94732 6.29387 8 6.15778 8C6.02168 8 5.89116 7.94732 5.79493 7.85355C5.69869 7.75979 5.64463 7.63261 5.64463 7.5V4.5C5.64463 4.36739 5.69869 4.24021 5.79493 4.14645C5.89116 4.05268 6.02168 4 6.15778 4Z"' + 'fill="black"/>' + "</svg>" + "<span>Delete</span></a>" + "</div></div>" + "</td>" + "<td>" + data[i].address + "</td>" + "<td>" + data[i].cNo + "</td>" + "<td>" + data[i].emailAddress + "</td>" + "<td>" + data[i].tier + "</td>" + "<td>" + data[i].userCount + "</td>" + "<td>" + data[i].vipCount + "</td>" + "<td>" + data[i].description + "</td>" + "<td><p>" + data[i].dateCreated + "</p><p>" + data[i].dateCreated + "</p></td>", + "</tr>"
        ]);
      }
      $.unblockUI();
    }).fail(function () {
      alert("There was an Error When Loading Data...");
    });
  }, 100);
}


