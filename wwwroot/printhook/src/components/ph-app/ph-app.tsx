import { Component, Host, h, State } from '@stencil/core';
import { ISettings, SettingsClient } from '../../clients/settings-client';
import { PrintClient, ILabelField } from '../../clients/print-client';

@Component({
  tag: 'ph-app',
  styleUrl: 'ph-app.scss',
  shadow: true,
})
export class PhApp {

  @State() settings: ISettings;
  @State() printers: string[] = [];
  @State() data: ILabelField[] = [];

  private readonly settingsClient: SettingsClient;
  private readonly printClient: PrintClient;

  constructor() {
    var port = Number.parseInt(window.location.search.split('port=')[1]);
    this.settingsClient = new SettingsClient(port);
    this.printClient = new PrintClient(port);
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

  private changeFieldName(field: ILabelField, value: string): void {
    var clonedData = [...this.data];
    var index = clonedData.indexOf(field);
    clonedData[index] = {...field, name: value};
    this.data = clonedData;
  }

  private changeFieldValue(field: ILabelField, value: string): void {
    var clonedData = [...this.data];
    var index = clonedData.indexOf(field);
    clonedData[index] = {...field, value: value};
    this.data = clonedData;
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
        <h2>Test a label</h2>
        <div class="form">
          {this.data &&
            <table>
              <tr>
                <th>Field</th>
                <th>Value</th>
                <th></th>
              </tr>
              {this.data.map(field =>
                <tr>
                  <td>
                    <input
                      type="text"
                      value={field.name}
                      onInput={e => this.changeFieldName(field, (e.target as HTMLInputElement).value)}
                    />
                  </td>
                  <td>
                    <input
                      type="text"
                      value={field.value}
                      onInput={e => this.changeFieldValue(field, (e.target as HTMLInputElement).value)}
                    />
                  </td>
                </tr>
              )}
            </table>
          }
          <div class="controls">
            <button onClick={() => this.data = [...this.data, {name: "", value: ""}]}>
              Add a field
            </button>
            {this.data && Object.keys(this.data).length > 0 &&
              <button onClick={() => this.printClient.print(this.data)}>
                Print
              </button>
            }
          </div>
        </div>
      </Host>
    );
  }
}
