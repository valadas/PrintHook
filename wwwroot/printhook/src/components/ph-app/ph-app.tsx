import { Component, Host, h, State } from '@stencil/core';
import { ISettings, SettingsClient } from '../../clients/settings-client';

@Component({
  tag: 'ph-app',
  styleUrl: 'ph-app.scss',
  shadow: true,
})
export class PhApp {

  @State() settings: ISettings;
  @State() printers: string[] = [];

  private readonly settingsClient: SettingsClient;

  constructor() {
    var port = Number.parseInt(window.location.search.split('port=')[1]);
    this.settingsClient = new SettingsClient(port);
  }

  componentWillLoad(){
    this.settingsClient.getPrinters()
    .then(printers => {
      this.printers = printers;
      this.settingsClient.getSettings()
      .then(settings => {
        this.settings = settings;
      })
      .catch((error) => {
        console.error(error);
      });
    })
    .catch(error => console.error(error));
  }

  private resetSettings(): void {
    this.settingsClient.getSettings()
    .then(settings => {
      this.settings = settings;
    })
    .catch(error => console.error(error));
  }

  render() {
    return (
      <Host>
        <h1>PrintHook Settings</h1>
        {this.settings &&
          <div class="form">
            <label>Printer:</label>
            <select
              onInput={e => this.settings = {...this.settings, PrinterName: (e.target as HTMLSelectElement).value}}
            >
              <option value="">-- Select a printer --</option>
              {this.printers.map(printer =>
                <option
                  value={printer}
                  selected={printer === this.settings.PrinterName}
                >
                  {printer}
                </option>)
              }
            </select>

            <label>Label file path:</label>
            <input
              type="text"
              value={this.settings.LabelFilePath}
              onInput={e => this.settings = {...this.settings, LabelFilePath: (e.target as HTMLInputElement).value}}
            />

            <label>Service Port:</label>
            <input
              type="number"
              min={1}
              max={65535}
              value={this.settings.Port}
              onInput={e => this.settings = {...this.settings, Port: Number.parseInt((e.target as HTMLInputElement).value)}}
            />
            <div class="controls">
              <button type="button"
                onClick={() => this.resetSettings()}>
                Cancel
              </button>
              <button
                type="button"
                onClick={() => this.settingsClient.saveSettings(this.settings).then(() => alert("Settings saved!"))}
              >
                Save
              </button>
            </div>
          </div>
        }
      </Host>
    );
  }
}
