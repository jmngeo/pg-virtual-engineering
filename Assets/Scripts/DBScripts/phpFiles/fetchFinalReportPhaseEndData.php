<?php

$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();



if (mysqli_connect_errno()) {
    echo 'Failed to connect to MySQL: ' . mysqli_connect_error();
}





$room_id = $_GET['room_id'];


$query = "SELECT score, phase_id FROM `phase_end_events` WHERE room_id =  '$room_id'";


$result = mysqli_query($mysqli, $query);
$rows = array();
while ($r = mysqli_fetch_assoc($result)) {
    $rows['data'][] = $r;
}



$json_rows = json_encode($rows);



echo $json_rows;




// CloseCon($mysqli);
?>