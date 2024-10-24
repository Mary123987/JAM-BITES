var turno = 0;
var juegoTerminado = true;
var grilla = [["", "", ""], ["", "", ""], ["", "", ""]];

function inicializar() {
    juegoTerminado = false;
    grilla = [["", "", ""], ["", "", ""], ["", "", ""]];
    var tabla = document.getElementById("grilla");
    var contenido = "";

    for (var i = 0; i < 3; i++) {
        contenido += "<tr>";
        for (var j = 0; j < 3; j++) {
            contenido += `<td id="${i}${j}" onclick="seleccionar(this);"></td>`;
        }
        contenido += "</tr>";
    }
    tabla.innerHTML = contenido;
    document.getElementById("ganador").innerHTML = "";  // Resetear mensaje
}

function seleccionar(celda) {
    var id = celda.getAttribute("id");
    const [i, j] = obtenerPosicionDesdeId(id);
    if (grilla[i][j] != "" || juegoTerminado) return;

    celda.innerHTML = '<img src="~/img/x.png" alt="X">';
    grilla[i][j] = "X";
    turno++;

    if (verificarGanador("X")) return;

    setTimeout(turnoComputadora, 500);  // Esperar 0.5s para la computadora
}

function turnoComputadora() {
    if (juegoTerminado) return;

    // Verificar si hay movimientos disponibles
    let movimientosPosibles = [];
    for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
            if (grilla[i][j] === "") {
                movimientosPosibles.push([i, j]);
            }
        }
    }

    // Si no hay movimientos disponibles, retornar
    if (movimientosPosibles.length === 0) {
        verificarGanador("O");
        return;
    }

    let i, j;
    
    // 30% de probabilidad de hacer un movimiento aleatorio
    if (Math.random() < 0.3) {
        // Elegir un movimiento aleatorio
        let movimientoAleatorio = movimientosPosibles[Math.floor(Math.random() * movimientosPosibles.length)];
        [i, j] = movimientoAleatorio;
    } else {
        // Usar minimax para el mejor movimiento
        let mejorMovimiento = minimax(grilla, "O");
        [i, j] = mejorMovimiento.posicion;
    }

    let celda = document.getElementById(i + "" + j);
    celda.innerHTML = '<img src="/workspaces/JAM-BITES/wwwroot/img/o.png" alt="O">';
    grilla[i][j] = "O";
    turno++;

    verificarGanador("O");
}

function minimax(tablero, jugador) {
    let ganador = obtenerGanador();
    if (ganador === "X") return { puntaje: -10, posicion: [-1, -1] };
    if (ganador === "O") return { puntaje: 10, posicion: [-1, -1] };
    if (tableroLleno()) return { puntaje: 0, posicion: [-1, -1] };

    let movimientos = [];
    for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
            if (tablero[i][j] === "") {
                tablero[i][j] = jugador;
                let puntaje = minimax(tablero, jugador === "O" ? "X" : "O").puntaje;
                movimientos.push({ posicion: [i, j], puntaje });
                tablero[i][j] = "";  // Deshacer el movimiento
            }
        }
    }

    // Si no hay movimientos disponibles
    if (movimientos.length === 0) {
        return { puntaje: 0, posicion: [-1, -1] };
    }

    if (jugador === "O") {
        return movimientos.reduce((a, b) => (a.puntaje > b.puntaje ? a : b));
    } else {
        return movimientos.reduce((a, b) => (a.puntaje < b.puntaje ? a : b));
    }
}

function obtenerPosicionDesdeId(id) {
    return [parseInt(id[0]), parseInt(id[1])];
}

function verificarGanador(jugador) {
    let ganador = obtenerGanador();
    
    if (ganador === jugador) {
        let divGanador = document.getElementById("ganador");
        if (jugador === "X") {
            let codigoDescuento = generarCodigoDescuento();
            divGanador.innerHTML = `<p class="mensaje">Felicidades ganaste!!!</p>
                                    <p class="sub-mensaje">Tu código de descuento es: <strong>${codigoDescuento}</strong></p>`;
        } else {
            divGanador.innerHTML = `<p class="mensaje">Perdiste :(</p>
                                    <p class="sub-mensaje">Gracias por participar</p>`;
        }
        juegoTerminado = true;
        return true;
    }
    
    // Verificar empate (cuando el tablero está lleno y no hay ganador)
    if (tableroLleno() && !ganador) {
        let divGanador = document.getElementById("ganador");
        divGanador.innerHTML = `<p class="mensaje">Perdiste :(</p>
                               <p class="sub-mensaje">Gracias por participar</p>`;
        juegoTerminado = true;
        return true;
    }
    
    return false;
}

function obtenerGanador() {
    for (let i = 0; i < 3; i++) {
        if (grilla[i][0] && grilla[i][0] === grilla[i][1] && grilla[i][1] === grilla[i][2])
            return grilla[i][0];
        if (grilla[0][i] && grilla[0][i] === grilla[1][i] && grilla[1][i] === grilla[2][i])
            return grilla[0][i];
    }
    if (grilla[0][0] && grilla[0][0] === grilla[1][1] && grilla[1][1] === grilla[2][2])
        return grilla[0][0];
    if (grilla[0][2] && grilla[0][2] === grilla[1][1] && grilla[1][1] === grilla[2][0])
        return grilla[0][2];
    return null;
}

function tableroLleno() {
    for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
            if (grilla[i][j] === "") return false;
        }
    }
    return true;
}

function generarCodigoDescuento() {
    let caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    let codigo = "";
    for (let i = 0; i < 8; i++) {
        codigo += caracteres.charAt(Math.floor(Math.random() * caracteres.length));
    }
    return codigo;
}