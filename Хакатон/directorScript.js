var modal = document.getElementById("myModal");
var btn = document.getElementById("createTask");
var span = document.getElementsByClassName("close")[0];
var send = document.getElementById("sendTask");
var items = document.getElementById("containerItems");
var userInput = document.getElementsByName("data");
var userData = document.getElementsByClassName("userData")[0];
const userName = window.sessionStorage.getItem("userName");
const userSurname = window.sessionStorage.getItem("userSurname");
let number_name = 0;
var numeric_modal = document.getElementById("Modal");

userData.innerHTML =
  "<i class='fa-solid fa-user'></i><h2 calss=`userName`>Имя:" +
  userName +
  "</h2><h2 class=`userSurname``>Фамилия:" +
  userSurname +
  "</h2>";

function CreateDiv(div) {
  `
  <div class="givenTask">
        </div>
  `;
}

btn.onclick = function () {
  modal.style.display = "block";
  send.style.visibility = "visible";
  for (let i = 0; i < userInput.length; i++) {
    userInput[i].value = null;
  }
};

span.onclick = function () {
  modal.style.display = "none";
};

window.onclick = function (event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
};

send.onclick = function () {
  var div = document.createElement("div");
  CreateDiv(div);
  dateInput = userInput[2].value;
  div.innerHTML =
    "<i class='fa-regular fa-clock'></i><h3>Срок сдачи: " +
    userInput[0].value +
    "</h3><h3>Название: " +
    dateInput +
    "</h3><button id=`btn_close`><h2>Закрыть задание</h2></button>";
  div.id = number_name;
  div.classList.add("givenTask");
  window.sessionStorage.setItem(
    number_name,
    JSON.stringify([userInput[0].value, userInput[1].value, dateInput])
  );
  items.appendChild(div);
  modal.style.display = "none";
  number_name++;

  div.getElementById("btn_close").onclick = function () {
    numeric_modal.style.display = "block";
  };

  div.onclick = function () {
    modal.style.display = "block";
    let dates = window.sessionStorage.getItem(div.id);
    dates = JSON.parse(dates);
    userInput[0].value = dates[0];
    userInput[1].value = dates[1];
    userInput[2].value = dates[2];
    send.style.visibility = "hidden";
  };
};
