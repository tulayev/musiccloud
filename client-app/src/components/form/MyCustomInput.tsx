import { Form } from 'semantic-ui-react';

interface Props {
  name: string;
  type: string;
  label?: string;
  handleChange?: (e: any) => void;
}

export default function MyCustomInput(props: Props) {
  return (
    <Form.Field>
      <label style={{color: '#ffffff'}}>{props.label}</label>
      <input name={props.name} type={props.type} onChange={props.handleChange} />
    </Form.Field>
  )
}
