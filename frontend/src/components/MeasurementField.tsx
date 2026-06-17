interface MeasurementFieldProps {
  label: string
  hint: string
  name: string
  value: string
  error?: string
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void
}

export default function MeasurementField({
  label,
  hint,
  name,
  value,
  error,
  onChange,
}: MeasurementFieldProps) {
  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '8px' }}>
      <div style={{ display: 'flex', flexDirection: 'column', gap: '4px' }}>
        <label style={{ fontFamily: 'Inter', fontWeight: 600, fontSize: '14px', color: '#000' }}>
          {label}
        </label>
        <span style={{ fontFamily: 'Inter', fontWeight: 400, fontSize: '14px', color: '#6B7280' }}>
          {hint}
        </span>
      </div>
      <input
        type="text"
        name={name}
        value={value}
        onChange={onChange}
        style={{
            width: '404px',
            height: '41px',
            paddingTop: '10px',
            paddingBottom: '10px',
            paddingLeft: '24px',
            paddingRight: '12px',
            backgroundColor: '#FAF6FF',
            border: error ? '1px solid #ef4444' : '1px solid #7424DA0D',
            borderRadius: '6px',
            fontFamily: 'Inter',
            fontSize: '14px',
            color: '#24102B',
            outline: 'none',
            boxSizing: 'border-box',
            }}
      />
      {error && (
        <span style={{ fontSize: '12px', color: '#ef4444' }}>{error}</span>
      )}
    </div>
  )
}