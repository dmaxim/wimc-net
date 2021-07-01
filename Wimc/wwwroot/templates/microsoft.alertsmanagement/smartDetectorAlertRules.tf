

// NOTE: This has a dependency on application insights instance
resource "azurerm_monitor_smart_detector_alert_rule" "alert_rule" {
    name = ""
    resource_group_name = ""
    severity = "Sev0"
    scope_resource_ids = "App Insights ids eg. [azurerm_application_insights.example.id]"
    frequency = "PT1M"
    detector_type = "FailureAnomaliesDetector"
  
    action_group {
      ids = []
    }
  
}