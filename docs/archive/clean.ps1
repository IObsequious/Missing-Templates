$VsVersion = '15.0'

Import-Module "$PSScriptRoot\solutionhelper.psm1" -Force

$solution = Get-CurrentSolution

$msbuild = Get-MSBuildLocation -TargetVsVersion $VsVersion

$SolutionDir = (Get-SolutionDir).TrimEnd('\')

$DevEnvDir = Get-DevEnvDir -TargetVersion $VsVersion

$args = "`"$solution`" /m /v:m /t:Clean /property:SolutionDir=`"$SolutionDir`" /property:DevEnvDir=`"$DevEnvDir`""

[System.Console]::WriteLine( "About to run command:" )
[System.Console]::WriteLine( "msbuild.exe $args" )

Start-Process $msbuild -ArgumentList $args -NoNewWindow -Wait

[System.Console]::WriteLine()
[System.Console]::Write( "Press any key to continue:" )
[System.Console]::Read()