<?php

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


$PhaseCount = intval($_GET['PhaseCount']);

if ($PhaseCount == 1) {

    $method1 = $_GET['method1'];
    $method2 = $_GET['method2'];
    $method3 = $_GET['method3'];

    $method1Quality = $_GET['method1Quality'];
    $method2Quality = $_GET['method2Quality'];
    $method3Quality = $_GET['method3Quality'];

    $extractPhaseEndReport =
        "SELECT * from finalreport where (method_name = '$method1' and quality = '$method1Quality') or (method_name = '$method2' and quality = '$method2Quality') or (method_name = '$method3' and quality = '$method3Quality')";
} else {
    $method1 = $_GET['method1'];
    $method2 = $_GET['method2'];

    $method1Quality = $_GET['method1Quality'];
    $method2Quality = $_GET['method2Quality'];

    $extractPhaseEndReport = "SELECT * from finalreport where (method_name = '$method1' and quality = '$method1Quality') or (method_name = '$method2' and quality = '$method2Quality')";
}




// echo 'playerID ' . $playerid;
// print_r($_GET);




$result = mysqli_query($mysqli, $extractPhaseEndReport);
$rows = array();
while ($r = mysqli_fetch_assoc($result)) {
    $rows['data'][] = $r;
}

$json_rows = json_encode($rows);
echo $json_rows;


// CloseCon($mysqli);
