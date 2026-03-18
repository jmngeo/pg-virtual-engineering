<?php

$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();

if (mysqli_connect_errno()) {
    echo 'Failed to connect to MySQL: ' . mysqli_connect_error();
}

$roomName = $_GET['roomName'];

$extractall = "SELECT
     SUM(method_1) as method_1,
     SUM(method_2) as method_2,
     SUM(method_3) as method_3,
     SUM(method_4) as method_4,
     SUM(method_5) as method_5,
     SUM(method_6) as method_6,
     SUM(method_7) as method_7,
     SUM(method_8) as method_8,
     SUM(method_9) as method_9
    FROM investments
    WHERE room_id = '$roomName'";

$result = mysqli_query($mysqli, $extractall);
$rows = array();
while ($r = mysqli_fetch_assoc($result)) {
    $rows['data'][] = $r;
}


$json_rows = json_encode($rows);

echo $json_rows;

// CloseCon($mysqli);
?>