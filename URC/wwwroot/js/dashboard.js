/**
 * Author:    Ping Cheng Chung
 * Partner:   Calvin Tu,Kevin Tran
 * Date:      11/29/2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Ping Cheng Chung - This work may not be copied for use in Academic Coursework.
 *
 * I, Ping Cheng Chung, certify that I wrote this code from scratch and did
 * not copy it in part or whole from another source.  Any references used
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 *
 *  drawn chart
 *
 *
 */





function drawRequiredSkilled() {
    //request for data
    $.ajax({
        type: 'GET',
        url: '/Dashboard/GetReuiredSkillData',

        success: function (response) {
            // Define the chart to be drawn.
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Key');
            data.addColumn('number', 'Value');
            
            var i;
            for (i = 0; i < response.length; i++) {
                data.addRow([response[i].Key, response[i].Value]);
            }




            var options = {
                'width': 500,
                'height': 350
            };


            // Instantiate and draw the chart.
            var chart_div = document.getElementById('chart_div_RS');
            var chart = new google.visualization.PieChart(chart_div);

            // Wait for the chart to finish drawing before calling the getImageURI() method.
            
            google.visualization.events.addListener(chart, 'ready', function () {
                chart_div.innerHTML = '<img id="img-1" src="' + chart.getImageURI() + '">';
               
            });
            chart.draw(data, options);

        }
    })
}



function drawCompareChart() {
    //request for data
    $.ajax({
        type: 'GET',
        url: '/Dashboard/GetCompareSkillsData',

        success: function (response) {
            // Define the chart to be drawn.
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Name');
            data.addColumn('number', 'Required Skill');
            data.addColumn('number', 'Student\'s Skill');
            var i;
            for (i = 0; i < response.length; i++) {
                data.addRow([response[i].name, response[i].reuquiredskill, response[i].studentskill]);
            }




            var options = {
                'width': 950,
                'height': 350
            };

            // Instantiate and draw the chart.
            var chart_div = document.getElementById('chart_div_Compare');
            var chart = new google.visualization.ColumnChart(chart_div);

            google.visualization.events.addListener(chart, 'ready', function () {
                chart_div.innerHTML = '<img id="img-3" src="' + chart.getImageURI() + '">';

            });



            chart.draw(data, options);

        }
    })
}










function drawStudentSkills() {
    //request for data
    $.ajax({
        type: 'GET',
        url: '/Dashboard/GetStudentSkillData',

        success: function (response) {
            // Define the chart to be drawn.
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Key');
            data.addColumn('number', 'amount');

            var i;
            for (i = 0; i < response.length; i++) {
                data.addRow([response[i].Key, response[i].Value]);
            }




            var options = {
                'width': 500,
                'height': 350
            };

            // Instantiate and draw the chart.
            var chart_div = document.getElementById('chart_div_SS');
            var chart = new google.visualization.PieChart(chart_div);

            google.visualization.events.addListener(chart, 'ready', function () {
                chart_div.innerHTML = '<img id="img-2" src="' + chart.getImageURI() + '">';

            });



            chart.draw(data, options);

        }
    })
}


function drawStudentInterest() {
    //request for data
    $.ajax({
        type: 'GET',
        url: '/Dashboard/GetStudentsInerestData',

        success: function (response) {
            // Define the chart to be drawn.
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Key');
            data.addColumn('number', 'Amount');

            var i;
            for (i = 0; i < response.length; i++) {
                data.addRow([response[i].Key, response[i].Value]);
            }




            var options = {
                'width': 500,
                'height': 300
            };

            // Instantiate and draw the chart.
            var chart = new google.visualization.ColumnChart(document.getElementById('chart_div_SI'));
            chart.draw(data, options);

        }
    })
}


$("#print-button").click(function () {
    window.print();
    }
);


$("#first-chart-1").click(function () {
    var Pagelink = "about:blank";
    var pwa = window.open(Pagelink, "_new");
    pwa.document.open();
    pwa.document.write(ImagetoPrint($("#img-1").attr('src')));
    pwa.document.close();

});





$("#second-chart-1").click(function () {
    var Pagelink = "about:blank";
    var pwa = window.open(Pagelink, "_new");
    pwa.document.open();
    pwa.document.write(ImagetoPrint($("#img-2").attr('src')));
    pwa.document.close();

});


function ImagetoPrint(source) {
    return "<html><head><scri" + "pt>function step1(){\n" +
        "setTimeout('step2()', 10);}\n" +
        "function step2(){window.print();window.close()}\n" +
        "</scri" + "pt></head><body onload='step1()'>\n" +
        "<img src='" + source + "' /></body></html>";
}