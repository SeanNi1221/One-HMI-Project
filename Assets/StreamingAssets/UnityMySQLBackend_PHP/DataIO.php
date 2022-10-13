<?php
header("Access-Control-Allow-Origin: *");

$servername = $_POST["serverName"];
$username = $_POST["userName"];
$password = $_POST["password"];
$sql = $_POST["sql"];

// Create connection
$conn = new mysqli($servername, $username, $password);

// Check connection
if ($conn->connect_error) {
  die("Error - Connection failed: " . $conn->connect_error);
}
// Return data
$result = $conn->query($sql) or die("Error - Query failed: " . $conn -> error);
$rows = array();
while($row = $result->fetch_assoc()) {
    $rows[] = $row;
}
echo json_encode($rows);

$conn->close();
?>