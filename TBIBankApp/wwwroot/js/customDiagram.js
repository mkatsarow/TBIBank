let invalidcount = document.getElementById('invalidcount').getAttribute('data-value');
let notreviewedcount = document.getElementById('notreviewedcount').getAttribute('data-value');
let newcount = document.getElementById('newcount').getAttribute('data-value');
let opencount = document.getElementById('opencount').getAttribute('data-value');
let closedcount = document.getElementById('closedcount').getAttribute('data-value');
let acceptedcount = document.getElementById('acceptedcount').getAttribute('data-value');
let rejectedcount = document.getElementById('rejectedcount').getAttribute('data-value');
updateChart();
function updateChart() {
    var chart = new CanvasJS.Chart("chartContainer", {


        theme: "light1",
        animationEnabled: false,
        title: {
            fontFamily: "Arial",
            text: "Email Chart by Status"
        },
        data: [
            {
                type: "pie",
                startAngle: 60,
                indexLabelFontSize: 17,
                indexLabel: "{label} - #percent%",
                toolTipContent: "<b>{label}:</b> {y} (#percent%)",

                dataPoints: [
                    { label: `Not Reviewed`, y: Number(notreviewedcount), color: "Blue" },
                    { label: `Invalid`, y: Number(invalidcount), color: "Red" },
                    { label: `New`, y: Number(newcount), color: "orange" },
                    { label: `Open`, y: Number(opencount), color: "Yellow" },
                    { label: `Closed`, y: Number(closedcount), color: "Chartreuse" }
                ],
            }
        ]

    });
    chart.render();
}


"use strict";

let connection = new signalR.HubConnectionBuilder().withUrl("/notification").build();


connection.on("UpdateChart", function (status, oldstatus) {
    console.log(status);
    console.log(oldstatus);
    if (status.toLowerCase() === "invalidapplication") {
        notreviewedcount = Number(notreviewedcount) - Number(1);
        invalidcount = Number(invalidcount) + Number(1);
    }
    else if (status.toLowerCase() === "notreviewed") {
        notreviewedcount = Number(notreviewedcount) + Number(1);
        invalidcount = Number(invalidcount) - Number(1);
    }
    else if (status.toLowerCase() === "new") {
        if (oldstatus === 1) {
            notreviewedcount = Number(notreviewedcount) - Number(1);
            newcount = Number(newcount) + Number(1);
        }
        else if (oldstatus === 4) {
            opencount = Number(opencount) - Number(1);
            newcount = Number(newcount) + Number(1);
        }
        else {
            newcount = Number(newcount) + Number(1);
            closedcount = Number(closedcount) - Number(1);
        }
    }
    else if (status.toLowerCase() === "open") {
        newcount = Number(newcount) - Number(1);
        opencount = Number(opencount) + Number(1);
    }
    if (status.toLowerCase() === "closed") {
        closedcount = Number(closedcount) + Number(1);
        opencount = Number(opencount) - Number(1);
    }
    updateChart();
});

connection.on("RecieveNewEmails", function (count) {
    console.log(count);
    notreviewedcount = Number(notreviewedcount) + Number(count);
    updateChart();
})
connection.on("LogOut", function (userName) {
    let div = document.getElementById(userName + '+remove');
    console.log(div);
    div.remove();
});
connection.on("UpdateOnline", function (data, count) {
    let div = document.getElementById("accordion");
    $(div).append(`<div id="${data.userName}+remove"> <div class="card">
                            <div class="card-header" id="headingThree">
                                <h5 class="mb-0">
                                    <button class="btn btn-link collapsed" data-toggle="collapse" data-target="#${data.userName}" aria-expanded="false" aria-controls="collapseThree">
                                        ${data.userName} - Operator
                                    </button>
                                </h5>
                            </div>
                            <div id="${data.userName}" class="collapse" aria-labelledby="headingThree" data-parent="#accordion">
                                <div class="card-body">
                                    <p>
                                        First name: ${data.firstName}
                                    </p>
                                    <p>
                                        Last name: ${data.lastName}
                                    </p>
                                    <p>
                                        Email: ${data.email}
                                    </p>
                                    <p>
                                        Count of Updated Emails: ${count}
                                    </p>

                                </div>
                            </div>
                        </div>
                            </div>`)
});
connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});


