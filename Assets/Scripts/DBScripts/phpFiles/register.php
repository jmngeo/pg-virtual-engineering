<?php

//$con = mysqli_connect('localhost', 'root', 'root', 'unityaccess');

//check the connection happened
/*
$db_host = 'localhost';
$db_user = 'root';
$db_password = '';
$db_db = 'seriousgame';
$db_port = 3306;
*/

// $db_host = 'sql7.freemysqlhosting.net';
// $db_user = 'sql7547540';
// $db_password = 'yBFAq7dttq';
// $db_db = 'sql7547540';
// $db_port = 3306;

// $mysqli = new mysqli($db_host, $db_user, $db_password, $db_db, $db_port);

$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();


if ($mysqli->connect_error) {
    echo 'Errno: ' . $mysqli->connect_errno;
    echo '<br>';
    echo 'Error: ' . $mysqli->connect_error;
    exit();
}

echo 'Success: A proper connection to MySQL was made.';
echo '<br>';
echo 'Host information: ' . $mysqli->host_info;
echo '<br>';
echo 'Protocol version: ' . $mysqli->protocol_version;

$playername = $_POST['playerName'];
$playerid = $_POST['playerID'];
$roomname = $_POST['roomName'];
$rolename = $_POST['roleName'];
$ismaster = $_POST['isMaster'];
$updatedby = $_POST['updatedBy'];
$createdby = $_POST['createdBy'];

//for methods

$method1 = $_POST['method1'];
$method2 = $_POST['method2'];
$method3 = $_POST['method3'];
$method4 = $_POST['method4'];
$method5 = $_POST['method5'];
$method6 = $_POST['method6'];
$method7 = $_POST['method7'];
$method8 = $_POST['method8'];
$method9 = $_POST['method9'];


$insertuserquery =
    "INSERT INTO players (playerID, playername, playerRole, isAdmin, roomName, updatedBy, createdBy) VALUES ('" .
    $playerid .
    "', '" .
    $playername .
    "', '" .
    $rolename .
    "', '" .
    $ismaster .
    "', '" .
    $roomname .
    "', '" .
    $updatedby .
    "', '" .
    $createdby .
    "')";

$insertinvestres =
    "INSERT INTO investments (player_id, method_1, method_2, method_3, method_4, method_5, method_6, method_7, method_8, method_9, createdby, updatedby, room_id ) VALUES 
    ('" . $playerid . "','0', '0', '0', '0', '0', '0', '0', '0', '0', '" . $createdby . "', '" . $updatedby . "', '" . $roomname . "')";

// $insertuserquery = "INSERT INTO players (playername, hash, salt) VALUES ('" . $playername . "',);";
if ($mysqli->query($insertuserquery)) {
    echo 'Records Updated';
} else {
    echo $mysqli->error;
}

if ($mysqli->query($insertinvestres)) {
    echo 'Records added';
} else {
    echo $mysqli->error;
}

error_reporting(0);


// CloseCon($mysqli);
