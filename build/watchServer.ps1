$originalLocation = Get-Location
$projectRoot = "$PSScriptRoot/../"

try {
  Set-Location -Path $projectRoot
  . ./build/util.ps1
  
  dotnet watch run --project "$webProjectFolder" --configuration 'Debug' --launch-profile 'Kestrel (Development)'

} finally {
  Set-Location $originalLocation
}
