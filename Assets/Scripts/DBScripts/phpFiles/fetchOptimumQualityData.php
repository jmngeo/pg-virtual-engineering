<?php

$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();



if (mysqli_connect_errno()) {
    echo 'Failed to connect to MySQL: ' . mysqli_connect_error();
}




$query = "SELECT 
    SUM(`StakeholderAnalyses_High` +  `EnvironmentModel_High` +  `ApplicationsScenario_High` +  `FunctionsHierarchy_High` +  `ActivityDiagram_High` +  `MorphologicalBox_High` +  `UtilityAnalysis_High` +  `LogicalArchitecture_High` +  `FMEA_High`) as total_optimum_sum, 
    SUM(`StakeholderAnalyses_High` + `EnvironmentModel_High` + `ApplicationsScenario_High`) as sum_phase_1,
    SUM(`FunctionsHierarchy_High` + `ActivityDiagram_High`) as sum_phase_2,
    SUM(`MorphologicalBox_High` + `UtilityAnalysis_High`) as sum_phase_3,
    SUM(`LogicalArchitecture_High` + `FMEA_High`) as sum_phase_4 FROM `quality_threshold`";


$result = mysqli_query($mysqli, $query);
$rows = array();
while ($r = mysqli_fetch_assoc($result)) {
    $rows['data'][] = $r;
}



$json_rows = json_encode($rows);



echo $json_rows;




// CloseCon($mysqli);
?>