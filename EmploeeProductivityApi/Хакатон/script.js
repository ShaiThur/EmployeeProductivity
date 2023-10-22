const leftHalf = document.getElementById("leftHalf");
const rightHalf = document.getElementById("rightHalf");
const body = document.getElementById("1");
const signIn = document.getElementsByName("signInWebSite");
const registration = document.getElementsByName("registration");
const button_reg = document.getElementById("btn_reg");
const button_sign = document.getElementById("btn_sign");
const checkbox = document.getElementById("input_checkbox");
const select = document.getElementById("select");
const userName = document.getElementById("userName_reg");
const userSurname = document.getElementById("userSurname_reg");

function FindInput(registration) {
  for (let i = 0; i < registration.length; i++) {
    if (registration[i].placeholder === "Отдел") {
      return registration[i];
    }
  }
}

var input = FindInput(registration);

checkbox.addEventListener("change", function () {
  if (this.checked) {
    console.log("Включил");
    select.style.visibility = "hidden";
    input.style.visibility = "visible";
  } else {
    console.log("Выключид");
    select.style.visibility = "visible";
    input.style.visibility = "hidden";
  }
});

button_reg.onclick = function () {
  window.sessionStorage.setItem("userName", userName.value);
  window.sessionStorage.setItem("userSurname", userSurname.value);
  if (checkbox.checked) {
    window.location.href = "./directorPage.html";
  } else {
    window.location.href = "./userPage.html";
  }
};

button_sign.onclick = function () {
  /* need Backend to route of website*/
};

function changeColor(event) {
  const x = event.clientX;
  const halfWidth = window.innerWidth / 2;

  if (x < halfWidth) {
    body.style.background = `linear-gradient(to right, #0186FE 50%, #ffffff 50%)`;
    leftHalf.style.backgroundColor = "#0186FE";
    leftHalf.style.color = "#FFFFFF";
    rightHalf.style.backgroundColor = "#FFFFFF";
    rightHalf.style.color = "#0186FE";
    button_sign.style.color = "grey";
    button_reg.style.color = "#FFFFFF";
    select.style.visibility = "hidden";
    checkbox.style.visibility = "hidden";
    for (let i = 0; i < registration.length; i++) {
      registration[i].placeholder = "";
      registration[i].value = "";
    }
    for (let i = 0; i < signIn.length; i++) {
      if (i === 0) {
        signIn[i].placeholder = "Логин";
      }
      if (i === 1) {
        signIn[i].placeholder = "Пароль";
      }

      //   var opt = document.createElement("option");
      //   opt.value = i;
      //   opt.innerHTML = i;
      //   select.appendChild(opt);
    }
  } else {
    body.style.background = `linear-gradient(to right, #ffffff 50%, #0186FE 50%)`;
    leftHalf.style.backgroundColor = "#FFFFFF";
    leftHalf.style.color = "#0186FE";
    if (checkbox.checked) {
      select.style.visibility = "hidden";
    } else {
      select.style.visibility = "visible";
    }
    button_sign.style.color = "#FFFFFF";
    button_reg.style.color = "grey";
    checkbox.style.visibility = "visible";
    rightHalf.style.backgroundColor = "#0186FE";
    rightHalf.style.color = "#FFFFFF";
    for (let i = 0; i < signIn.length; i++) {
      signIn[i].placeholder = "";
      signIn[i].value = "";
    }
    for (let i = 0; i < registration.length; i++) {
      if (i === 0) {
        registration[i].placeholder = "Логин";
      }
      if (i === 1) {
        registration[i].placeholder = "Пароль";
      }
      if (i === 2) {
        registration[i].placeholder = "Имя";
      }
      if (i === 3) {
        registration[i].placeholder = "Фамилия";
      }
      if (i === 4) {
        registration[i].placeholder = "Отдел";
      }
    }
  }
}

document.addEventListener("mousemove", changeColor);
