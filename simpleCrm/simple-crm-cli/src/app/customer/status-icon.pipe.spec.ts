import { StatusIconPipe } from './status-icon.pipe';

fdescribe('StatusIconPipe', () => {
  it('create an instance', () => {
    const pipe = new StatusIconPipe();
    expect(pipe).toBeTruthy();
  });

  it('Prospect (lowercase) should result in running', () => {
    const pipe = new StatusIconPipe(); // 1. SETUP: construct a new instance of the class.
    const x = pipe.transform('runner'); // 2. INVOKE the method
    expect(x).toEqual('running'); // 3. VERIFY the result of the method matches what is expected.
 });
  it('Prospect (mixed case) should result in running', () => {
    const pipe = new StatusIconPipe(); // 1. SETUP: construct a new instance of the class.
    const x = pipe.transform('RuNnEr'); // 2. INVOKE the method
    expect(x).toEqual('running'); // 3. VERIFY the result of the method matches what is expected.
  });
  it('Prospect (mixed case) should result in running', () => {
    const pipe = new StatusIconPipe(); // 1. SETUP: construct a new instance of the class.
    const x = pipe.transform('Runner'); // 2. INVOKE the method
    expect(x).toEqual('running'); // 3. VERIFY the result of the method matches what is expected.
  });

  it('Prospect (lowercase) should result in cycling', () => {
    const pipe = new StatusIconPipe(); // 1. SETUP: construct a new instance of the class.
    const x = pipe.transform('cyclist'); // 2. INVOKE the method
    expect(x).toEqual('cycling'); // 3. VERIFY the result of the method matches what is expected.
 });
  it('Prospect (mixed case) should result in cycling', () => {
    const pipe = new StatusIconPipe(); // 1. SETUP: construct a new instance of the class.
    const x = pipe.transform('Cyclist'); // 2. INVOKE the method
    expect(x).toEqual('cycling'); // 3. VERIFY the result of the method matches what is expected.
  });
  it('Prospect (mixed case) should result in cycling', () => {
    const pipe = new StatusIconPipe(); // 1. SETUP: construct a new instance of the class.
    const x = pipe.transform('CycList'); // 2. INVOKE the method
    expect(x).toEqual('cycling'); // 3. VERIFY the result of the method matches what is expected.
  });

  it('Prospect blank (empty string) should result in sleeping', () => {
    const pipe = new StatusIconPipe(); // 1. SETUP: construct a new instance of the class.
    const x = pipe.transform(''); // 2. INVOKE the method
    expect(x).toEqual('sleeping'); // 3. VERIFY the result of the method matches what is expected.
  });
  it('Prospect null should result in sleeping', () => {
    const pipe = new StatusIconPipe(); // 1. SETUP: construct a new instance of the class.
    const x = pipe.transform(null); // 2. INVOKE the method
    expect(x).toEqual('sleeping'); // 3. VERIFY the result of the method matches what is expected.
  });
  it('Prospect null should result in sleeping', () => {
    const pipe = new StatusIconPipe(); // 1. SETUP: construct a new instance of the class.
    const x = pipe.transform(undefined); // 2. INVOKE the method
    expect(x).toEqual('sleeping'); // 3. VERIFY the result of the method matches what is expected.
  });
});

