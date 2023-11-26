
[System.IO.DirectoryInfo] $installerFopiScriptRoot = $PSScriptRoot
set-location $installerFopiScriptRoot


function Read-EslintRc-Json([System.IO.DirectoryInfo] $dir) {
    [System.IO.FileInfo] $file = Join-Path -Path $dir -ChildPath ".eslintrc.json"
    [PSCustomObject] $content = Get-Content -Path $file | ConvertFrom-Json

    [PSCustomObject] $instance = @{
        file    = $file; 
        content = $content; 
    }
    $instance | Add-Member -MemberType ScriptMethod -Name Save -Value {
        Set-Content  -Path $this.file -Value ($this.content | ConvertTo-Json -Depth 10)
        & "$BinNodeModules/prettier" $this.file --write
    }
    return $instance
}

try {
    Write-Host "generate workspace"
    npx create-nx-workspace@latest fopi --preset=react-monorepo --appName=client --bundler=webpack --style=scss --pm=yarn --e2eTestRunner=none --no-nxCloud 
    #nx@17.1.3 all required options are set, we don't need the '--no-interactive' option
    set-location ./fopi
    [System.IO.DirectoryInfo] $workspaceLocation = (Get-Location).Path
    $BinNodeModules = "$workspaceLocation/node_modules/.bin"

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

    Write-Host "generate layers"
    yarn nx generate @nx/react:library --name=application --unitTestRunner=dom --bundler=rollup --directory=libs/fopi/application --importPath=@BoobaFetes/fopi-application --projectNameAndRootFormat=as-provided --publishable=true --tags=type:application --no-interactive
    yarn nx generate @nx/react:library --name=react-infra --unitTestRunner=jest --bundler=rollup --directory=libs/fopi/react-infra --importPath=@BoobaFetes/fopi-react-infra --projectNameAndRootFormat=as-provided --publishable=true --tags=type:adapter --no-interactive
    yarn nx generate @nx/js:library --name=infra --unitTestRunner=jest --directory=libs/fopi/infra --importPath=@BoobaFetes/fopi-infra --projectNameAndRootFormat=as-provided --publishable=true --tags=type:infra --no-interactive
    yarn nx generate @nx/js:library --name=domain --unitTestRunner=jest --directory=libs/fopi/domain --importPath=@BoobaFetes/fopi-domain --projectNameAndRootFormat=as-provided --publishable=true --tags=type:domain --no-interactive

    Write-Host "add npm dependencies"
    yarn add @apollo/client apollo-link-rest graphql qs clsx react-helmet http-status-codes
    yarn add -D @types/react-helmet
}
finally {
    Set-Location $installerFopiScriptRoot
}