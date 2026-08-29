{{- define "modpack-manager.labels" -}}
{{ include "modpack-manager.selectorLabels" . }}
app.kubernetes.io/version: {{ .Chart.Version | quote }}
app.kubernetes.io/part-of: {{ .Values.productName | quote }}
app.kubernetes.io/managed-by: {{ .Release.Service | quote }}
{{- end -}}

{{- define "modpack-manager.selectorLabels" -}}
app.kubernetes.io/name: {{ .Values.productName | quote }}
app.kubernetes.io/instance: {{ .Release.Name | quote }}
{{- end -}}

{{- define "modpack-manager.podSecurity" -}}
automountServiceAccountToken: false
securityContext:
  runAsNonRoot: true
  seccompProfile:
    type: RuntimeDefault
{{- end -}}

{{- define "modpack-manager.containerSecurity" -}}
securityContext:
  runAsNonRoot: true
  runAsUser: {{ .Values.runAsUser }}
  readOnlyRootFilesystem: true
  allowPrivilegeEscalation: false
  capabilities:
    drop: ["ALL"]
{{- end -}}