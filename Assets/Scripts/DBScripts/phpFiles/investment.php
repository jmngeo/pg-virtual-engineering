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

$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();


// $db_host = 'sql7.freemysqlhosting.net';
// $db_user = 'sql7547540';
// $db_password = 'yBFAq7dttq';
// $db_db = 'sql7547540';
// $db_port = 3306;

// $mysqli = new mysqli($db_host, $db_user, $db_password, $db_db, $db_port);

// if ($mysqli->connect_error) {
//     // echo 'Errno: ' . $mysqli->connect_errno;
//     // echo '<br>';
//     // echo 'Error: ' . $mysqli->connect_error;
//     exit();
// }

// echo 'Success: A proper connection to MySQL was made.';
// echo '<br>';
// echo 'Host information: ' . $mysqli->host_info;
// echo '<br>';
// echo 'Protocol version: ' . $mysqli->protocol_version;

// echo playerID;


$playerid = $_GET['playerID'];
$playername = $_GET['playerName'];

$method1 = $_GET['method1'];
$method2 = $_GET['method2'];
$method3 = $_GET['method3'];

// echo 'playerID ' . $playerid;
// print_r($_GET);

$extractresources =
    "SELECT * from investments where player_id = $playerid";


$result = mysqli_query($mysqli, $extractresources);
$rows = array();
while ($r = mysqli_fetch_assoc($result)) {
    $rows['data'][] = $r;
}




$json_rows = json_encode($rows);
echo $json_rows;


// CloseCon($mysqli);
