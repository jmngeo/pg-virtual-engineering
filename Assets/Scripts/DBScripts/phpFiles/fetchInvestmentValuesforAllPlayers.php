<?php

$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();



if (mysqli_connect_errno()) {
    echo 'Failed to connect to MySQL: ' . mysqli_connect_error();
}




$roomName = $_GET['roomName'];


$extractall = "SELECT players.playerID, players.playername, players.playerRole, players.roomName, investments.method_1, investments.method_2, investments.method_3, investments.method_4, investments.method_5, investments.method_6, investments.method_7, investments.method_8, investments.method_9 FROM players INNER JOIN investments ON players.playerID = investments.player_id  WHERE players.roomName = '$roomName'";

$result = mysqli_query($mysqli, $extractall);
$rows = array();
while ($r = mysqli_fetch_assoc($result)) {
    $rows['data'][] = $r;
}



$json_rows = json_encode($rows);



echo $json_rows;




// CloseCon($mysqli);
?>