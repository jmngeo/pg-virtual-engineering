<?php

// $db_host = 'sql7.freemysqlhosting.net';
// $db_user = 'sql7547540';
// $db_password = 'yBFAq7dttq';
// $db_db = 'sql7547540';
// $db_port = 3306;


// $mysqli = new mysqli($db_host, $db_user, $db_password, $db_db, $db_port);

// if(mysqli_connect_errno()) {
//     echo 'Failed to connect to MySQL: ' . mysqli_connect_error();
// }

// $path = '/DB_Connections.php';


$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();


$quality_threshold = "SELECT * FROM quality_threshold";

$result = mysqli_query($mysqli, $quality_threshold);
$rows = array();
while ($r = mysqli_fetch_assoc($result)) {
    $rows['data'][] = $r;
}


$json_rows = json_encode($rows);
echo $json_rows;


// CloseCon($mysqli);
