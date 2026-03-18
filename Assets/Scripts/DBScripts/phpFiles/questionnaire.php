<?php

// $db_host = 'sql7.freemysqlhosting.net';
// $db_user = 'sql7547540';
// $db_password = 'yBFAq7dttq';
// $db_db = 'sql7547540';
// $db_port = 3306;


// $mysqli = new mysqli($db_host, $db_user, $db_password, $db_db, $db_port);

$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();

$PhaseCount = intval($_GET['PhaseCount']);

if (mysqli_connect_errno()) {
    echo 'Failed to connect to MySQL: ' . mysqli_connect_error();
}


if ($PhaseCount == 1) {

    $method_1 = "StakeholderAnalyses";
    $method_2 = "EnvironmentModel";
    $method_3 = "ApplicationsScenario";


} elseif ($PhaseCount == 2) {

    $method_1 = "FunctionsHierarchy";
    $method_2 = "ActivityDiagram";

} elseif ($PhaseCount == 3) {

    $method_1 = "MorphologicalBox";
    $method_2 = "UtilityAnalysis";


} elseif ($PhaseCount == 4) {

    $method_1 = "LogicalArchitecture";
    $method_2 = "FMEA";

}


if ($PhaseCount == 1) {
    $questionnaire = "(SELECT * FROM questionnaire WHERE method = '" . $method_1 . "' AND quality = 'low' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1) 
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_1 . "' AND quality = 'medium' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1)
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_1 . "' AND quality = 'high' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1)
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_2 . "' AND quality = 'low' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1) 
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_2 . "' AND quality = 'medium' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1)
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_2 . "' AND quality = 'high' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1)
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_3 . "' AND quality = 'low' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1) 
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_3 . "' AND quality = 'medium' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1)
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_3 . "' AND quality = 'high' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1)
    UNION (SELECT * FROM questionnaire WHERE quality = 'random' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1)";
} else {
    $questionnaire = "(SELECT * FROM questionnaire WHERE method = '" . $method_1 . "' AND quality = 'low' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1) 
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_1 . "' AND quality = 'medium' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 2)
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_1 . "' AND quality = 'high' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1)
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_2 . "' AND quality = 'low' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 2)
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_2 . "' AND quality = 'medium' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 2)
    UNION (SELECT * FROM questionnaire WHERE method = '" . $method_2 . "' AND quality = 'high' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1)
    UNION (SELECT * FROM questionnaire WHERE quality = 'random' AND phase = '" . $PhaseCount . "' ORDER BY RAND() LIMIT 1)";
}


// Fetch all the questions for a phase
// Group the questions by level 

$result = mysqli_query($mysqli, $questionnaire);
$rows = array();
while ($r = mysqli_fetch_assoc($result)) {
    $rows['data'][] = $r;
}

// function utf8ize($d) {
//     if (is_array($d)) {
//         foreach ($d as $k => $v) {
//             $d[$k] = utf8ize($v);
//         }
//     } else if (is_string ($d)) {
//         return utf8_encode($d);
//     }
//     return $d;
// }

$json_rows = json_encode($rows);
echo $json_rows;


// CloseCon($mysqli);
