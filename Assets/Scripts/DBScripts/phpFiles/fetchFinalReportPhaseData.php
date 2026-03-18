<?php

$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();



if (mysqli_connect_errno()) {
    echo 'Failed to connect to MySQL: ' . mysqli_connect_error();
}





$room_id = $_GET['room_id'];


$query = "SELECT 
	SUM(method_1 +  method_2 +  method_3 +  method_4 +  method_5 +  method_6 +  method_7 +  method_8 +  method_9) as total_sum, 
    SUM(method_1 + method_2 + method_3) as sum_phase_1,
    SUM(method_4 + method_5) as sum_phase_2,
    SUM(method_6 + method_7) as sum_phase_3,
    SUM(method_8 + method_9) as sum_phase_4
    FROM `investments` WHERE room_id = '$room_id'";


$result = mysqli_query($mysqli, $query);
$rows = array();
while ($r = mysqli_fetch_assoc($result)) {
    $rows['data'][] = $r;
}



$json_rows = json_encode($rows);



echo $json_rows;




// CloseCon($mysqli);
?>