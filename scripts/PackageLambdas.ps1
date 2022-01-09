param (      
    [Parameter(Mandatory = $true)] [String]$repoFolder 
)

function Build-Projects() {
    $path = Resolve-Path -Path $repoFolder
    $solutions = Get-ChildItem -Path $path -Recurse -Include "*.csproj"
    foreach ($file in $solutions) {
        Write-Host "Building '$file'..."
        dotnet restore $file
        dotnet build -c Release $file 
    }
}

function Compress-Lambdas() {
    $basePath = Resolve-Path -Path $repoFolder 
    $solutions = Get-ChildItem -Path $basePath -Recurse -Include "*.csproj"
    Write-Host "Generating $basePath"
    foreach ($file in $solutions) {
        $xmlFile = [xml](Get-Content $file)
        $lambdaParam = $xmlFile.Project.PropertyGroup | Where-Object AWSProjectType -eq 'Lambda' | Select-Object AWSProjectType
        if ($lambdaParam.AWSProjectType -eq "Lambda") {
            $projectPath = $file.Directory
            Set-Location $projectPath
            $zipFile = $file.BaseName
            Write-Host "Generating $basePath/artifacts/$zipFile.zip"
            dotnet lambda package -c Release -o "$basePath/artifacts/$zipFile.zip" /p:PreserveCompilationContext=true

            if (!$?) { exit -1 }
        }
        else {
            Write-Host "Skipping '$file'."
        }
    }
}

$currentPath = Get-Location  
Build-Projects
Compress-Lambdas
Set-Location $currentPath