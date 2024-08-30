{{- define "sqlserver.name" -}}
{{ .Chart.Name }}
{{- end -}}

{{- define "sqlserver.fullname" -}}
{{ include "sqlserver.name" . }}-{{ .Release.Name }}
{{- end -}}

{{- define "sqlserver.labels" -}}
app.kubernetes.io/name: {{ include "sqlserver.name" . }}
helm.sh/chart: {{ .Chart.Name }}-{{ .Chart.Version | replace "+" "_" }}
app.kubernetes.io/instance: {{ .Release.Name }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end -}}
