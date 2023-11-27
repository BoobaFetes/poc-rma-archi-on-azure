param(
    [string] $name
)

[System.IO.DirectoryInfo] $rootScriptRoot = "$PSScriptRoot/.."
set-location $rootScriptRoot

function Get-Bin-Directory-From-NodeModules {
    return [System.IO.DirectoryInfo]"$rootScriptRoot/$name/web/$name/node_modules/.bin"
}
function Read-EslintRc-Json([System.IO.DirectoryInfo] $dir) {
    [System.IO.FileInfo] $file = Join-Path -Path $dir -ChildPath ".eslintrc.json"
    [PSCustomObject] $content = Get-Content -Path $file | ConvertFrom-Json

    [PSCustomObject] $instance = @{
        file    = $file; 
        content = $content; 
    }
    $instance | Add-Member -MemberType ScriptMethod -Name Save -Value {
        Set-Content  -Path $this.file -Value ($this.content | ConvertTo-Json -Depth 10)
        & "$(Get-Bin-Directory-From-NodeModules)/prettier" $this.file --write
    }
    return $instance
}

try {
    Write-Host "create $name directory"
    [void](mkdir $name)
    set-location ./$name
    
    Write-Host "create api directory"
    [void](mkdir api)
    Write-Output '' > ./api/.gitkeep 
    Write-Host "    > you will have to create the back end by your own with VisualStudio scaffolders"
    Read-Host "(press enter to continue)"
    
    Write-Host "create web directory"
    [void](mkdir web)
    set-location ./web

    Write-Host "generate workspace with NX"
    npx create-nx-workspace@17.1.3 $name --preset=react-monorepo --appName=client --bundler=webpack --style=scss --pm=yarn --e2eTestRunner=none --no-nxCloud 
    #nx@17.1.3 all required options are set, we don't need the '--no-interactive' option

    [System.IO.DirectoryInfo] $workspaceLocation = "$((Get-location).Path)/$name"
    set-location $workspaceLocation

    Write-Host "generate layers"
    Invoke-expression "yarn nx generate @nx/react:library --name=application --unitTestRunner=jest --bundler=rollup --directory=libs/$name/application --importPath=@BoobaFetes/$name-application --projectNameAndRootFormat=as-provided --publishable=true --tags=type:application --no-interactive"
    Invoke-expression "yarn nx generate @nx/react:library --name=react-infra --unitTestRunner=jest --bundler=rollup --directory=libs/$name/react-infra --importPath=@BoobaFetes/$name-react-infra --projectNameAndRootFormat=as-provided --publishable=true --tags=type:adapter --no-interactive"
    Invoke-expression "yarn nx generate @nx/js:library --name=infra --unitTestRunner=jest --directory=libs/$name/infra --importPath=@BoobaFetes/$name-infra --projectNameAndRootFormat=as-provided --publishable=true --tags=type:infra --no-interactive"
    Invoke-expression "yarn nx generate @nx/js:library --name=domain --unitTestRunner=jest --directory=libs/$name/domain --importPath=@BoobaFetes/$name-domain --projectNameAndRootFormat=as-provided --publishable=true --tags=type:domain --no-interactive"
    
    Write-Host "stop daemon"
    yarn nx daemon --stop

    Write-Host "generate library boundaries"
    [PSCustomObject] $esLintFile = Read-EslintRc-Json -dir $workspaceLocation
    $libraryBoundaries = $esLintFile.content.overrides[0].rules."@nx/enforce-module-boundaries"[1]
    $libraryBoundaries.depConstraints = [array]@(
        @{
            sourceTag                = "scope:application"
            onlyDependOnLibsWithTags = @("scope:application", "scope:react-infra", "scope:infra", "scope:domain")
        },
        @{
            sourceTag                = "scope:react-infra"
            onlyDependOnLibsWithTags = @("scope:react-infra", "scope:infra", "scope:domain")
        },
        @{
            sourceTag                = "scope:infra"
            onlyDependOnLibsWithTags = @("scope:infra", "scope:domain")
        },
        @{
            sourceTag                = "scope:domain"
            onlyDependOnLibsWithTags = @("scope:domain")
        }
    )
    $esLintFile.Save()

    Write-Host "add npm dependencies"
    yarn add @apollo/client apollo-link-rest graphql qs clsx react-helmet http-status-codes
    yarn add -D @types/react-helmet
    
    Write-Host "your workspace '$name' is created."
}
finally {
    Set-Location $rootScriptRoot
}
Read-Host "(press enter to continue)"
