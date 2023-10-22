var btn = document.getElementById("btn_show");
var container = document.getElementById("container");

btn.onclick = function () {
  container.innerHTML = "";
  // set chart theme
  anychart.theme("darkBlue");
  // create column chart
  var chart = anychart.column();

  // turn on chart animation
  chart.animation(true);

  // set chart title text settings
  chart.title("Rating of workers");

  fetch("https://localhost:7183/DayRating")
    .then((response) => response.json())
    .then((post) => {
      const result = post;
      var arr = [];
      for (var key in result) {
        if (result.hasOwnProperty(key)) {
          arr.push([key, result[key]]);
        }
      }
      series = chart.column(arr);
      console.log(post);
    });

  // create area series with passed data
  var series = chart.column();

  // set series tooltip settings
  series.tooltip().titleFormat("{%X}");

  series
    .tooltip()
    .position("center-top")
    .anchor("center-bottom")
    .offsetX(0)
    .offsetY(5)
    .format("{%Value}{groupsSeparator: }");

  // set scale minimum
  chart.yScale().minimum(0);

  // set yAxis labels formatter
  chart.yAxis().labels().format("{%Value}{groupsSeparator: }");

  // tooltips position and interactivity settings
  chart.tooltip().positionMode("point");
  chart.interactivity().hoverMode("by-x");

  // axes titles
  chart.xAxis().title("Name");
  chart.yAxis().title("Tasks");

  // set container id for the chart
  chart.container("container");

  // initiate chart drawing
  chart.draw();
};
