var eje1;
var eje2;
var eje3;
var eje4;
var eje5;
var varlorEje1;
var varlorEje2;
var varlorEje3;
var varlorEje4;
var varlorEje5;
var btnPinza;
var chkPuntos;
var brazo;
const uri = 'https://localhost:5001/Api/Arm'
var canvas = null;
var txtAreaJson;



window.onload = inicializar


function inicializar() {

    eje1 = document.getElementById('coor1');
    eje2 = document.getElementById('coor2');
    eje3 = document.getElementById('coor3');
    eje4 = document.getElementById('coor4');
    eje5 = document.getElementById('coor5');
    varlorEje1 = document.getElementById('varlorEje1');
    varlorEje2 = document.getElementById('varlorEje2');
    varlorEje3 = document.getElementById('varlorEje3');
    varlorEje4 = document.getElementById('varlorEje4');
    varlorEje5 = document.getElementById('varlorEje5');
    canvas = document.getElementById("canvasBrazo");
    btnPinza = document.getElementById("btnPinza");
    txtAreaJson = document.getElementById('txtAreaJson');
    chkPuntos = document.getElementById("chkPuntos");
    cargarEventos();
    mostrarValorRanges();
    cargaInicial();

};

function cargarEventos() {
    eje1.addEventListener('change', pintarBrazos);
    eje2.addEventListener('change', pintarBrazos);
    eje3.addEventListener('change', pintarBrazos);
    eje4.addEventListener('change', pintarBrazos);
    eje5.addEventListener('change', pintarBrazos);

    eje1.addEventListener('input', pintarBrazos);
    eje2.addEventListener('input', pintarBrazos);
    eje3.addEventListener('input', pintarBrazos);
    eje4.addEventListener('input', pintarBrazos);
    eje5.addEventListener('input', pintarBrazos);

    eje1.addEventListener('click', pintarBrazos);
    eje2.addEventListener('click', pintarBrazos);
    eje3.addEventListener('click', pintarBrazos);
    eje4.addEventListener('click', pintarBrazos);
    eje5.addEventListener('click', pintarBrazos);

    btnPinza.addEventListener('click', pintarBrazos);
    chkPuntos.addEventListener('change', pintarBrazos)

}

async function pintarBrazos(e) {
    const v = await modificarBrazo(e);
}

function mostrarValorRanges() {
    varlorEje1.innerHTML = eje1.value;
    varlorEje2.innerHTML = eje2.value;
    varlorEje3.innerHTML = eje3.value;
    varlorEje4.innerHTML = eje4.value;
    varlorEje5.innerHTML = eje5.value;
}

async function modificarBrazo(e) {
    var idControl = e.currentTarget.id;
    let item;

    item = {
        Angle: 0,
        Angle2: 0,
        Angle3: 0,
        Angle4: 0,
        Angle5: 0,
        StatusGripper: brazo.gripper.status
    }


    switch (idControl) {
        case "coor1":
            item.Angle = parseInt(eje1.value)
            break
        case "coor2":
            item.Angle2 = parseInt(eje2.value)
            break
        case "coor3":
            item.Angle3 = parseInt(eje3.value)
            break
        case "coor4":
            item.Angle4 = parseInt(eje4.value)
            break
        case "coor5":
            item.Angle5 = parseInt(eje5.value)
            break
        case "btnPinza":
            item.StatusGripper = !brazo.gripper.status;
            break

    }


    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(data => {
            brazo = data;
            actualizarDibujo(data)
            mostrarValorRanges();
            mostrarJSON(data);
        })
        .catch(error => console.error('Unable to add item.', error));
}



async function actualizarDibujo(data) {
    var clean = canvas.getContext("2d");
    clean.beginPath();
    clean.clearRect(0, 0, canvas.width, canvas.height);
    clean.stroke();
    iniciarCanvas(data);
}

async function cargaInicial() {
    inicializarBrazo();
}


async function inicializarBrazo() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

async function _displayItems(data) {
    brazo = data
    eje1.value = data.joints[0].angle;
    eje2.value = data.joints[1].angle;
    eje3.value = data.joints[2].angle;
    eje4.value = data.joints[3].angle;
    iniciarCanvas(data);
}

async function iniciarCanvas(data) {
    var ctx = canvas.getContext("2d");
    ctx.beginPath();


    ctx.stroke();

    var canvasContext = canvas.getContext('2d');
    //lienas de Base
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#fff';
    canvasContext.lineWidth = 20;
    canvasContext.moveTo(data.joints[0].initialPoint.x - 50, data.joints[0].initialPoint.y)
    canvasContext.lineTo(data.joints[0].initialPoint.x + 50, data.joints[0].initialPoint.y)
    canvasContext.stroke();

    //lienas ejes x
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#fff';
    canvasContext.lineWidth = 2;
    canvasContext.moveTo(data.joints[0].initialPoint.x - 250, data.joints[0].initialPoint.y)
    canvasContext.lineTo(data.joints[0].initialPoint.x + 250, data.joints[0].initialPoint.y)
    canvasContext.stroke();

    //lienas ejes y
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#fff';
    canvasContext.lineWidth = 2;
    canvasContext.moveTo(data.joints[0].initialPoint.x, data.joints[0].initialPoint.y - 400)
    canvasContext.lineTo(data.joints[0].initialPoint.x, data.joints[0].initialPoint.y + 165)
    canvasContext.stroke();


    // Draw the red line.
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#f00';
    canvasContext.lineWidth = 10;
    canvasContext.moveTo(data.joints[0].initialPoint.x, data.joints[0].initialPoint.y);
    canvasContext.lineTo(data.joints[0].finalPoint.x, data.joints[0].finalPoint.y);
    canvasContext.arc(data.joints[0].finalPoint.x, data.joints[0].finalPoint.y, 5, 0, 2 * Math.PI);
    canvasContext.stroke();



    // Draw the green line.
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#0f0';
    canvasContext.lineWidth = 10;
    canvasContext.moveTo(data.joints[1].initialPoint.x, data.joints[1].initialPoint.y);
    canvasContext.lineTo(data.joints[1].finalPoint.x, data.joints[1].finalPoint.y);
    canvasContext.arc(data.joints[1].finalPoint.x, data.joints[1].finalPoint.y, 5, 0, 2 * Math.PI);
    canvasContext.stroke();

    // Draw the green line.
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#0c8';
    canvasContext.lineWidth = 10;
    canvasContext.moveTo(data.joints[2].initialPoint.x, data.joints[2].initialPoint.y);
    canvasContext.lineTo(data.joints[2].finalPoint.x, data.joints[2].finalPoint.y);
    canvasContext.arc(data.joints[2].finalPoint.x, data.joints[2].finalPoint.y, 5, 0, 2 * Math.PI);
    canvasContext.stroke();

    // Draw the green line.
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#c13f23';
    canvasContext.lineWidth = 10;
    canvasContext.moveTo(data.joints[3].initialPoint.x, data.joints[3].initialPoint.y);
    canvasContext.lineTo(data.joints[3].finalPoint.x, data.joints[3].finalPoint.y);
    canvasContext.arc(data.joints[3].finalPoint.x, data.joints[3].finalPoint.y, 5, 0, 2 * Math.PI);
    canvasContext.stroke();

    // Grper base
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#48116a';
    canvasContext.lineWidth = 10;
    canvasContext.moveTo(data.gripper.baseGripper.x, data.gripper.baseGripper.y);
    canvasContext.lineTo(data.gripper.baseGripperA.x, data.gripper.baseGripperA.y);
    //canvasContext.arc(data.joints[3].finalPoint.x, data.joints[3].finalPoint.y, 5, 0, 2 * Math.PI);
    canvasContext.stroke();

    // Grper base
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#48116a';
    canvasContext.lineWidth = 10;
    canvasContext.moveTo(data.gripper.baseGripper.x, data.gripper.baseGripper.y);
    canvasContext.lineTo(data.gripper.baseGripperB.x, data.gripper.baseGripperB.y);
    //canvasContext.arc(data.joints[3].finalPoint.x, data.joints[3].finalPoint.y, 5, 0, 2 * Math.PI);
    canvasContext.stroke();

    // Grper base
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#48116a';
    canvasContext.lineWidth = 10;
    canvasContext.moveTo(data.gripper.baseGripperA.x, data.gripper.baseGripperA.y);
    canvasContext.lineTo(data.gripper.baseGripperC.x, data.gripper.baseGripperC.y);
    //canvasContext.arc(data.joints[3].finalPoint.x, data.joints[3].finalPoint.y, 5, 0, 2 * Math.PI);
    canvasContext.stroke();

    // Grper base
    canvasContext.beginPath();
    canvasContext.strokeStyle = '#48116a';
    canvasContext.lineWidth = 10;
    canvasContext.moveTo(data.gripper.baseGripperB.x, data.gripper.baseGripperB.y);
    canvasContext.lineTo(data.gripper.baseGripperD.x, data.gripper.baseGripperD.y);
    //canvasContext.arc(data.joints[3].finalPoint.x, data.joints[3].finalPoint.y, 5, 0, 2 * Math.PI);
    canvasContext.stroke();


    if (chkPuntos.checked) {
        canvasContext.beginPath();
        canvasContext.strokeStyle = '#fff';
        canvasContext.fillText(data.joints[0].initialPoint.x + "," + data.joints[0].initialPoint.y, data.joints[0].initialPoint.x + 10, data.joints[0].initialPoint.y + 10);
        canvasContext.fillText(data.joints[1].initialPoint.x + "," + data.joints[1].initialPoint.y, data.joints[1].initialPoint.x + 10, data.joints[1].initialPoint.y + 10);
        canvasContext.fillText(data.joints[2].initialPoint.x + "," + data.joints[2].initialPoint.y, data.joints[2].initialPoint.x + 10, data.joints[2].initialPoint.y + 10);
        canvasContext.fillText(data.joints[3].initialPoint.x + "," + data.joints[3].initialPoint.y, data.joints[3].initialPoint.x + 10, data.joints[3].initialPoint.y + 10);
        canvasContext.fillText(data.gripper.baseGripper.x + "," + data.gripper.baseGripper.y, data.gripper.baseGripper.x + 10, data.gripper.baseGripper.y + 10);

        canvasContext.stroke();

    }
}

function mostrarJSON(data) {
    document.getElementById('tdJson01').innerHTML = "";
    document.getElementById('tdJson02').innerHTML = "";
    document.getElementById('tdJson03').innerHTML = "";
    document.getElementById('tdJson04').innerHTML = "";
    document.getElementById('tdJson05').innerHTML = "";

    let regeStr01 = '';
    let regeStr02 = '';
    let regeStr03 = '';
    let regeStr04 = '';
    let regeStr05 = '';
    jsonStrn01 = JSON.stringify(data.joints[0]);
    jsonStrn02 = JSON.stringify(data.joints[1]);
    jsonStrn03 = JSON.stringify(data.joints[2]);
    jsonStrn04 = JSON.stringify(data.joints[3]);
    jsonStrn05 = JSON.stringify(data.gripper);

    f = {
        brace: 0
    };

    regeStr01 = jsonStrn01.replace(/({|}[,]*|[^{}:]+:[^{}:,]*[,{]*)/g, function(m, p1) {
        var rtnFn = function() {
                return '<div style="text-indent: ' + (f['brace'] * 20) + 'px;">' + p1 + '</div>';
            },
            rtnStr = 0;
        if (p1.lastIndexOf('{') === (p1.length - 1)) {
            rtnStr = rtnFn();
            f['brace'] += 1;
        } else if (p1.indexOf('}') === 0) {
            f['brace'] -= 1;
            rtnStr = rtnFn();
        } else {
            rtnStr = rtnFn();
        }
        return rtnStr;
    });

    regeStr02 = jsonStrn02.replace(/({|}[,]*|[^{}:]+:[^{}:,]*[,{]*)/g, function(m, p1) {
        var rtnFn = function() {
                return '<div style="text-indent: ' + (f['brace'] * 20) + 'px;">' + p1 + '</div>';
            },
            rtnStr = 0;
        if (p1.lastIndexOf('{') === (p1.length - 1)) {
            rtnStr = rtnFn();
            f['brace'] += 1;
        } else if (p1.indexOf('}') === 0) {
            f['brace'] -= 1;
            rtnStr = rtnFn();
        } else {
            rtnStr = rtnFn();
        }
        return rtnStr;
    });

    regeStr03 = jsonStrn03.replace(/({|}[,]*|[^{}:]+:[^{}:,]*[,{]*)/g, function(m, p1) {
        var rtnFn = function() {
                return '<div style="text-indent: ' + (f['brace'] * 20) + 'px;">' + p1 + '</div>';
            },
            rtnStr = 0;
        if (p1.lastIndexOf('{') === (p1.length - 1)) {
            rtnStr = rtnFn();
            f['brace'] += 1;
        } else if (p1.indexOf('}') === 0) {
            f['brace'] -= 1;
            rtnStr = rtnFn();
        } else {
            rtnStr = rtnFn();
        }
        return rtnStr;
    });

    regeStr04 = jsonStrn04.replace(/({|}[,]*|[^{}:]+:[^{}:,]*[,{]*)/g, function(m, p1) {
        var rtnFn = function() {
                return '<div style="text-indent: ' + (f['brace'] * 20) + 'px;">' + p1 + '</div>';
            },
            rtnStr = 0;
        if (p1.lastIndexOf('{') === (p1.length - 1)) {
            rtnStr = rtnFn();
            f['brace'] += 1;
        } else if (p1.indexOf('}') === 0) {
            f['brace'] -= 1;
            rtnStr = rtnFn();
        } else {
            rtnStr = rtnFn();
        }
        return rtnStr;
    });

    regeStr05 = jsonStrn05.replace(/({|}[,]*|[^{}:]+:[^{}:,]*[,{]*)/g, function(m, p1) {
        var rtnFn = function() {
                return '<div style="text-indent: ' + (f['brace'] * 20) + 'px;">' + p1 + '</div>';
            },
            rtnStr = 0;
        if (p1.lastIndexOf('{') === (p1.length - 1)) {
            rtnStr = rtnFn();
            f['brace'] += 1;
        } else if (p1.indexOf('}') === 0) {
            f['brace'] -= 1;
            rtnStr = rtnFn();
        } else {
            rtnStr = rtnFn();
        }
        return rtnStr;
    });

    //txtAreaJson.innerHTML += regeStr;
    document.getElementById('tdJson01').innerHTML += regeStr01;
    document.getElementById('tdJson02').innerHTML += regeStr02;
    document.getElementById('tdJson03').innerHTML += regeStr03;
    document.getElementById('tdJson04').innerHTML += regeStr04;
    document.getElementById('tdJson05').innerHTML += regeStr05;
}