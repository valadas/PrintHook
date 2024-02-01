export class SettingsClient{
    private readonly basePath;

    constructor(port?: number) {
        this.basePath = `http://localhost:${port || 9000}/api/Settings/`;
    }

    public async getPrinters(): Promise<string[]> {
        var path = `${this.basePath}GetPrinters`;
        var response = await fetch(path, {method: 'GET'});
        return await response.json();
    }

    public async getSettings(): Promise<ISettings> {
        var path = `${this.basePath}GetSettings`;
        var response = await fetch(path, {method: 'GET'});
        return await response.json();
    }

    public async saveSettings(settings: ISettings): Promise<void> {
        var path = `${this.basePath}SaveSettings`;
        await fetch(
            path,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(settings)
            });
    }
};

export interface ISettings {
    /** The file path to the label. */
    LabelFilePath: string;

    /** The name of the printer. */
    PrinterName: string;

    /** The port of the webservice. */
    Port: number;
};