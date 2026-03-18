<?php

// $db_host = 'sql7.freemysqlhosting.net';
// $db_user = 'sql7547540';
// $db_password = 'yBFAq7dttq';
// $db_db = 'sql7547540';
// $db_port = 3306;

// $mysqli = new mysqli($db_host, $db_user, $db_password, $db_db, $db_port);

// if ($mysqli->connect_error) {
//     echo 'Errno: ' . $mysqli->connect_errno;
//     echo '<br>';
//     echo 'Error: ' . $mysqli->connect_error;
//     exit();
// }

$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();


echo 'Success: A proper connection to MySQL was made.';
echo '<br>';
echo 'Host information: ' . $mysqli->host_info;
echo '<br>';
echo 'Protocol version: ' . $mysqli->protocol_version;


$score = $_POST['score'];
$phase_id = intval($_POST['phase_id']);
$room_id = $_POST['room_id'];

//for methods

$insertuserquery = "INSERT INTO phase_end_events (phase_id, score, room_id) VALUES ('" . $phase_id . "', '" . $score . "', '" . $room_id . "')";



if ($mysqli->query($insertuserquery)) {
    echo 'Records Updated';
} else {
    echo $mysqli->error;
}


// CloseCon($mysqli);