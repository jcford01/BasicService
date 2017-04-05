Param (
	[switch]$uninstall = $false
)

Begin {
	function ATG-Stop-Services {
		Param (
			[Parameter(ValueFromPipeline=$true)]
			[string] $match
		)
		Begin {
			Write-Host "Stoping Service $match" -ForegroundColor Yellow
		}
		Process {
			get-service | where {$_.Name -like "$match"} | where {$_.Status -eq 'Running'} | stop-service -pass 
		}
	}

	function ATG-Start-Services {
		Param (
			[Parameter(ValueFromPipeline=$true)]
			[string] $match
		)
		Begin {
			Write-Host "Start Service $match" -ForegroundColor Yellow
		}
		Process {
			try {
				get-service | where {$_.Name -like "$match"} | where {$_.Status -ne 'Running'} | % { 
					Write-Host $_.Name
					start-service -Verbose $_.Name  
					Start-Sleep -s 5
				} 

				Start-Sleep -s 2

				get-service | where {$_.Name -like "$match"} | where {$_.Status -ne 'Running'} | start-service -pass 
			}
			catch [system.exception] {
				Write-Host $_
			}
		}
	}

	function ATG-Uninstall-Service {
		Param (
			[Parameter(ValueFromPipeline=$true)]
			[string] $match
		)
		Begin {
			ATG-Stop-Services $match
			Write-Host "uninstalling Service $match" -ForegroundColor Red
		}
		Process {
			Get-WmiObject -Class Win32_Service | where {$_.Name -like "$match"} | % {
			    Write-Host $_.Name  
			    $_.delete()
			} | Out-Null

		}
	}

	function ATG-Install-Service {
		Param (
			[Parameter(ValueFromPipeline=$true)]
			[string] $match,
			[string] $path
		)
		Begin {
			ATG-Uninstall-Service -match $match
			Write-Host "Installing Service $match" -ForegroundColor Yellow

			Start-Sleep -s 5
		}
		Process {
			New-Service -Name $match -DisplayName $match -BinaryPathName "$($path)"
			ATG-Start-Services -match $match;
			Write-Host "Complet $match" -ForegroundColor Green
		}
	}
}



Process {
	$name = "Armstrong.BasicService"
	$path = (Get-Item -Path ".\" -Verbose).FullName
	$path = $path -replace "\Scripts", ""
	$path = "$($path)BasicService.exe"
	if ($uninstall -eq $true) {
		ATG-Uninstall-Service -match $name
	} else  {
		ATG-Install-Service -match $name -path $path
	}
}