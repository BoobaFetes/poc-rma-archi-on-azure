import { render } from '@testing-library/react';

import ReactInfra from './react-infra';

describe('ReactInfra', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<ReactInfra />);
    expect(baseElement).toBeTruthy();
  });
});
