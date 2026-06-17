interface ScoreResultProps {
  score: number
}

export default function ScoreResult({ score }: ScoreResultProps) {
  return (
    <div
      style={{
        width: '404px',
        height: '58px',
        borderRadius: '10px',
        border: '1px solid #7424DA66',
        padding: '16px',
        boxSizing: 'border-box',
        display: 'flex',
        alignItems: 'center',
      }}
    >
      <p style={{ margin: 0, fontFamily: 'Inter', fontWeight: 400, fontSize: '20px', lineHeight: '130%', color: '#351B44' }}>
        News score: <strong style={{ fontWeight: 600 }}>{score}</strong>
      </p>
    </div>
  )
}