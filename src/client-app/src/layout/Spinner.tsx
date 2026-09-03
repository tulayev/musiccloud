import { Dimmer, Loader } from 'semantic-ui-react';

interface Props {
    inverted?: boolean;
    content?: string;
}

export default function Spinner({inverted = true, content = 'Загрузка...'}: Props) {
    return (
      <Dimmer 
        active={true} 
        inverted={inverted}
      >
        <Loader content={content} />
      </Dimmer>
    )
}